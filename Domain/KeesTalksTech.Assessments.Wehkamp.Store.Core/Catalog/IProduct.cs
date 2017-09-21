using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog
{
    public interface IProduct
    {
        string Id { get; }

        string Name { get; }

        string Description { get; }
        
        string ThumbnailUrl { get; }

        string PictureUrl { get; }

        string[] Keywords { get; }

        IPrice Price { get; }
    }
}