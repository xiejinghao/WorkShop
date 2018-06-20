using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_HW.Dao;


namespace MVC_HW.Models.Service
{
    public class ProductService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Product> GetAllProduct()
        {
            ProductDao dao = new ProductDao();
            return dao.GetAllProduct();
        }

        public decimal GetPrice(int productID)
        {
            ProductDao dao = new ProductDao();
            return dao.GetPrice(productID);
        }
    }
}
