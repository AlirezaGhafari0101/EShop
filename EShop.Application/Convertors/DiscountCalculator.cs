using EShop.Domain.Models.Order;


namespace EShop.Application.Convertors
{
    public class DiscountCalculator
    {
        public static double PurchaseProfit(IEnumerable<OrderDetail> orderDetails)
        {
            double purchaseProfit=0;
            foreach (var item in orderDetails)
            {
                if (item.Product?.Discount != null && DateTime.Now >= item.Product.Discount.StartDate && DateTime.Now <= item.Product.Discount.EndDate)
                {
                    purchaseProfit += ((double)item.Color.Price * (double)((double)item.Product.Discount.DiscountPercentage / (double)100))* (double)item.Count;
                }
            }

            return purchaseProfit;
        }



        public static double TotalShopingCart(IEnumerable<OrderDetail> orderDetails)
        {

            double result = 0;
            foreach (var item in orderDetails)
            {
                if (item.Product?.Discount != null && DateTime.Now >= item.Product.Discount.StartDate && DateTime.Now <= item.Product.Discount.EndDate)
                {
                    double calcDiscountPrice = 0;
                    calcDiscountPrice = ((double)item.Color.Price * (double)((double)item.Product.Discount.DiscountPercentage / (double)100)) * (double)item.Count;
                    result += item.Price - calcDiscountPrice;
                }
                if (item.Product?.Discount == null || DateTime.Now <= item.Product.Discount.StartDate || DateTime.Now >= item.Product.Discount.EndDate)
                {
                    result += item.Price;
                }

            }

            return result;
        }



    }
}
