using System.Collections.Generic;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Core.Shopping
{
    public interface IBasketService
    {
        IBasket Create();

        IBasket Get(string identifier);

        IList<IBasket> GetAll(uint page, uint pageSize);

        void Store(IBasket basket);

        bool Validate(IBasket basket);
    }
}