using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using FlickrNet;

public partial class Photos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            string defaultKeyword = "USABWarriors";

            GetPhotos(defaultKeyword);
        }
    }

    protected void GetPhotos(string tag)
    {

        PhotoSearchOptions options = new PhotoSearchOptions();

        options.Page = 1;
        options.PerPage = 1000;
        options.SortOrder = PhotoSearchSortOrder.DatePostedDescending;
        options.MediaType = MediaType.Photos;
        options.Extras = PhotoSearchExtras.All;
        options.Tags = tag;

        string url;

        Flickr flickr = new Flickr(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["shardSecret"]);
        FlickrNet.Cache.CacheDisabled = true;
        PhotoCollection photos = flickr.PhotosSearch(options);
        foreach (Photo ph in photos)
        {
            url = ph.MediumUrl;
        }

        ThumbnailsList.DataSource = photos;
        ThumbnailsList.DataBind();

    }
}