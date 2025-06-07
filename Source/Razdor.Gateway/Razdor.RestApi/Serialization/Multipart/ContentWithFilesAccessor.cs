using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Razdor.RestApi.Multipart;

public class ContentWithFilesAccessor(
    IHttpContextAccessor contextAccessor,
    IOptions<JsonOptions> jsonOptions
)
{
    private HttpContext Context => contextAccessor.HttpContext!;

    public async Task<ContentWithFiles<TMainContent>> ParseAsync<TMainContent>(CancellationToken cancellationToken = default)
    {
        if (!MediaTypeHeaderValue.TryParse(Context.Request.ContentType, out MediaTypeHeaderValue? contentType))
            throw new BadHttpRequestException("Missing content-type");

        TMainContent content;
        if (contentType.MediaType.StartsWith(MediaTypeNames.Application.Json, StringComparison.OrdinalIgnoreCase))
        { 
            content = await ParseMainContentAsync<TMainContent>(Context.Request.Body,  cancellationToken);
            return new ContentWithFiles<TMainContent>(content, AsyncEnumerable.Empty<MultipartRequestFile>());
        }

        if (!contentType.MediaType.StartsWith(MediaTypeNames.Multipart.FormData, StringComparison.OrdinalIgnoreCase))
        {
            throw new BadHttpRequestException("Not supported content type");
        }
        
        string boundary = GetBoundary(contentType, lengthLimit: 70);
        var multipartReader = new MultipartReader(boundary, Context.Request.Body);
        
        MultipartSection? section = await multipartReader.ReadNextSectionAsync(cancellationToken);

        if (section == null)
            throw new BadHttpRequestException("Missing main content section");
        
        content = await ParseMainContentAsync<TMainContent>(section.Body, cancellationToken);
        return new ContentWithFiles<TMainContent>(content, AwaitParseFiles(multipartReader));
    }

    private static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
    {
        var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);
        if (StringSegment.IsNullOrEmpty(boundary))
        {
            throw new BadHttpRequestException("Missing content-type boundary.");
        }
        if (boundary.Length > lengthLimit)
        {
            throw new BadHttpRequestException($"Multipart boundary length limit {lengthLimit} exceeded.");
        }
        return boundary.ToString();
    }

    private async Task<TMainContent> ParseMainContentAsync<TMainContent>(
        Stream content, CancellationToken cancellationToken = default
    ){
        return await JsonSerializer.DeserializeAsync<TMainContent>(
            content,
            jsonOptions.Value.SerializerOptions,
            cancellationToken
        ) ?? throw new BadHttpRequestException("Missing content json");   
    }
    
    private async IAsyncEnumerable<MultipartRequestFile> AwaitParseFiles(MultipartReader multipartReader)
    {
        while (await multipartReader.ReadNextSectionAsync() is { } section)
        {
            if(!MediaTypeHeaderValue.TryParse(section.ContentType, out MediaTypeHeaderValue? sectionType) || !sectionType.MediaType.HasValue)
                throw new BadHttpRequestException("Invalid content type in section " + section.ContentType);
            
            if (!ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue? disposition))
                throw new BadHttpRequestException("Invalid content disposition in section " + section.ContentDisposition);

            if (!disposition.FileName.HasValue || !disposition.Name.HasValue)
                throw new BadHttpRequestException("Disposition Name and FileName is Required");

            MemoryStream memoryStream = new MemoryStream();
            await section.Body.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            
             yield return new MultipartRequestFile(
                disposition.Name.Value,
                disposition.FileName.Value,
                sectionType.MediaType.Value,
                memoryStream
            );
        }
                
    }
}