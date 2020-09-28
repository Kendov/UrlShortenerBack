using System.Collections.Generic;
using urlShortener.Models;

namespace urlShortener.Services
{
    public interface IUrlDataService
    {
        List<UrlData> List();
        UrlData Get(string Id);
        string GetUrl(string Id);
        UrlData Post(UrlData entry);
        
    }
}