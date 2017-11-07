using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using Microsoft.EntityFrameworkCore;
using HostelService;
using HostelManager.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostelManager.Controllers.Admin
{
    public class ServicePersonController : BaseController<ServicePersonModel>
    {
        protected override DbSet<ServicePersonModel> dbSet => hostelContext.ServicePersons;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new ServicePersonModel());
        }

        [HttpPost]
        public JsonResult Persons()
        {
            ContextService<ServicePersonModel> service = new ContextService<ServicePersonModel>(dbSet, hostelContext);
            var list = service.GetEntitys(d => true).Select(d => new { id = d.Id, text = d.RealName });
            return Json(list);
        }


        [HttpPost]
        public JsonResult Upload([FromServices] IHostingEnvironment env, UploadImageModel model)
        {
            var result = hostelContext.ServicePersons.FirstOrDefault(d => d.GUID == model.GUID);
            if (result == null)
            {
                return Json(new { state = false, message = "未找到该人员信息，信息错误", code = 40004 });
            }
            string base64 = (model.data);
            string fileName = Path.Combine($"upload/{model.GUID}");
            try
            {
                byte[] arr2 = Convert.FromBase64String(base64.Split(',')[1]);
                string path = Path.Combine(env.WebRootPath, fileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileName += $"/{model.type}.jpg";
                System.IO.File.WriteAllText($"{path}/{model.type}_{DateTime.Now.ToFileTime()}.txt", base64);
                System.IO.File.WriteAllBytes($"{path}/{model.type}.jpg", arr2);
                if (model.type == "ICardPositive")
                {
                    result.ICardPositive = fileName;
                }
                else if (model.type == "ICardBack")
                {
                    result.ICardBack = fileName;
                }
                else if (model.type == "Icon")
                {
                    result.Icon = fileName;
                }
                else
                {
                    result.Health = fileName;
                }
                hostelContext.SaveChanges();
            }
            catch (Exception ex)
            {

                return Json(new { state = false, message = ex.Message });
            }

            return Json(new { state = true, message = "图片上传成功" });
        }
    }
}
