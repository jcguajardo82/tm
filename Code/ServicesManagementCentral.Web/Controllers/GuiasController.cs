using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.Guias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    public class GuiasController : Controller
    {
        // GET: Guias
        public ActionResult Index()
        {
            return View();
        }
        private void CrearCotizacionesLogyt(List<ProductEmbalaje> Products, int orderNo)
        {
            string productsAll = string.Empty;
            bool bigTicket = false;
            decimal sumPeso = 0, width = 0, length = 0, heigth = 0;
            DataTable dt = DALGuias.GetTableProducts();

            foreach (var p in Products)
            {
                productsAll += p.ProductId.ToString() + ",";
            }
            List<WeightByProducts> lstPesos = DataTableToModel.ConvertTo<WeightByProducts>(DALGuias.GetDimensionsByProducts(productsAll).Tables[0]);

            Session["ListWeightByProducts"] = lstPesos;

            foreach (var item in lstPesos)
            {
                if (item.PesoVol > item.Peso)
                {
                    if (item.PesoVol > 70)
                        bigTicket = true;

                    var piezas = Products.Where(x => x.ProductId == item.Product).FirstOrDefault().Pieces;
                    sumPeso += (item.PesoVol * piezas);
                    width += item.Width;
                    length += item.Lenght;
                    heigth += item.Height;

                    dt.Rows.Add(item.Product, item.PesoVol, item.Cve_CategSAP, item.Cve_GciaCategSAP, item.Cve_GpoCategSAP, item.Desc_CategSAP);
                }
                else
                {
                    if (item.Peso > 70)
                        bigTicket = true;

                    var piezas = Products.Where(x => x.ProductId == item.Product).FirstOrDefault().Pieces;
                    sumPeso += (item.Peso * piezas);
                    width += item.Width;
                    length += item.Lenght;
                    heigth += item.Height;

                    dt.Rows.Add(item.Product, item.Peso, item.Cve_CategSAP, item.Cve_GciaCategSAP, item.Cve_GpoCategSAP, item.Desc_CategSAP);
                }


            }

            if (sumPeso < 1)
                sumPeso = 1;

            var widthRound = decimal.Round(width);
            if (widthRound > 1)
                Session["SumWidth"] = decimal.ToInt32(widthRound);
            else
                Session["SumWidth"] = 1;

            var lengthRound = decimal.Round(length);
            if (lengthRound > 1)
                Session["SumLength"] = decimal.ToInt32(lengthRound);
            else
                Session["SumLength"] = 1;

            var heightRound = decimal.Round(heigth);
            if (heightRound > 1)
                Session["SumHeigth"] = decimal.ToInt32(heightRound);
            else
                Session["SumHeigth"] = 1;

            Session["SumPeso"] = sumPeso;

            DALGuias.GuardarTarifasLogyt(orderNo, sumPeso, bigTicket, dt);

        }
    }
}