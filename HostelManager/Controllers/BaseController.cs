using HostelManager.Models;
using HostelModel;
using HostelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;


namespace HostelManager.Controllers
{
    public abstract class BaseController<T> : Controller , IDisposable where T : BaseModel
    {
        protected HostelContext hostelContext;
        public BaseController()
        {
            hostelContext = new HostelContext();
        }
        protected abstract DbSet<T> dbSet { get; }

        [HttpPost]
        public JsonResult Details(int id)
        {
            try
            {
                ContextService<T> service = new ContextService<T>(dbSet, hostelContext);
                var obj = service.GetEntity(d => d.Id == id);
                return Json(new { state = true, message = "操作成功", data = obj });
            }
            catch
            {
                return Json(new { state = false, message = "获取数据详情错误" });

            }

        }

        [HttpPost]
        public JsonResult Remove([FromBody]IEnumerable<T> id)
        {
            try
            {
                ContextService<T> service = new ContextService<T>(dbSet, hostelContext);
                List<Expression<Func<T, bool>>> list = new List<Expression<Func<T, bool>>>();
                foreach (var item in id)
                {
                    Expression<Func<T, bool>> right = d => d.GUID == item.GUID;
                    list.Add(right);
                }
                service.RemoveEntity(list);
                return Json(new { state = true, message = "" });
            }
            catch 
            {
                return Json(new { state = false, message = "删除数据错误" });

            }

        }
        [HttpPost]
        public JsonResult Update(T model)
        {
            try
            {
                ContextService<T> service = new ContextService<T>(dbSet, hostelContext);
                service.UpdateEntity(model, d => d.GUID == model.GUID);

                return Json(new { state = true, message = "操作成功" });
            }
            catch
            {
                return Json(new { state = false, message = "更新错误" });

            }
        }

        [HttpPost]
        public JsonResult Create(T model)
        {
            try
            {
                ModelState.Remove("GUID"); 
                model.GUID = Guid.NewGuid().ToString("N");
                if (!ModelState.IsValid) {
                    return Json(new { state = false, message = "输入验证不合法" });
                }
                 
                ContextService<T> service = new ContextService<T>(dbSet, hostelContext);
                service.AddEntity(model);

                return Json(new { state = true, message = "操作成功" });
            }
            catch 
            {
                return Json(new { state = false, message = "数据操作服务器错误，请确认数据是否完整" });


            }
        }


        [HttpPost]
        public JsonResult List()
        {
            ContextService<T> service = new ContextService<T>(dbSet, hostelContext);
            var list = service.GetEntitys(d => true);
            return Json(list);
        }

        protected override void Dispose(bool disposing)
        {
            if (hostelContext != null)
            {
                try
                {
                    hostelContext.Dispose();
                }
                catch (Exception)
                {

                   
                }
             
            }
        }

    }




}