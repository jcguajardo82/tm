using ExcelDataReader;
using ServicesManagement.Web.DAL;
using ServicesManagement.Web.Helpers;
using ServicesManagement.Web.Models.Impex;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServicesManagement.Web.Controllers
{
    public class ImpexController : Controller
    {
        // GET: Impex
        public ActionResult Index()
        {
            return View();
        }

        #region TransPlazas
        public ActionResult TransPlazas()
        {
            return View();
        }

        public ActionResult GetTransPlazas()
        {
            try
            {
                List<TransportistaPlazas> list = new List<TransportistaPlazas>();

                var ds = DALImpex.upCorpTms_Cns_TransportistaPlazas();

                if (ds.Tables.Count > 0)
                {
                    list = DataTableToModel.ConvertTo<TransportistaPlazas>(ds.Tables[0]);
                }

                var result = new { Success = true, resp = list };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception x)
            {
                var result = new { Success = false, Message = x.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ImportFileTransPlazas(HttpPostedFileBase importFile)
        {
            if (importFile == null) return Json(new { Status = 0, Message = "No File Selected" });

            try
            {
                var fileData = GetDataFromFileTransPlazas(importFile.InputStream);



                var dt = fileData.ToDataTable();
                //var tblEmployeeParameter = new SqlParameter("tblEmployeeTableType", SqlDbType.Structured)
                //{
                //    TypeName = "dbo.tblTypeEmployee",
                //    Value = dtEmployee
                //};
                //await _dbContext.Database.ExecuteSqlCommandAsync("EXEC spBulkImportEmployee @tblEmployeeTableType", tblEmployeeParameter);
                DALImpex.upCorpTms_Ins_TransportistaPlazas(dt);
                return Json(new { Status = 1, Message = "File Imported Successfully " });
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Message = ex.Message });
            }
        }

        private List<TransportistaPlazas> GetDataFromFileTransPlazas(Stream stream)
        {
            var List = new List<TransportistaPlazas>();
            try
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names    
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        foreach (DataRow objDataRow in dataTable.Rows)
                        {
                            if (objDataRow.ItemArray.All(x => string.IsNullOrEmpty(x?.ToString()))) continue;
                            List.Add(new TransportistaPlazas()
                            {
                                IdTransportista = Convert.ToInt32(objDataRow[0].ToString()),
                                IdPlaza = Convert.ToInt32(objDataRow[1].ToString()),
                                Desc_Plaza = objDataRow[2].ToString(),
                                PostalCode = objDataRow[3].ToString(),
                                IdTipoEnvio = Convert.ToInt32(objDataRow[4].ToString()),
                                bitDeleted = Convert.ToBoolean(objDataRow[5].ToString()),
                                CreatedId = User.Identity.Name
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return List;
        }
        #endregion
    }
}