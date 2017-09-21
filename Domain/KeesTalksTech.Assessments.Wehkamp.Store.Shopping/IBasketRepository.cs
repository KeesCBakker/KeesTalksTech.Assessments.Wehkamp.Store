using System.Collections.Generic;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Shopping
{
    public interface IBasketRepository
    {
        Basket Get(string identifier);

        IList<Basket> GetAll(uint page, uint pageSize);

        void Add(Basket basket);
    }
}
