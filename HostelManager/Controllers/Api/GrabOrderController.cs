using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostelModel;
using HostelManager.Models;

namespace HostelManager.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/GrabOrder")]
    public class GrabOrderController : BaseApiController
    {
        /// <summary>
        /// 获取服务人员订单房间号列表
        /// </summary>
        /// <param name="id">PersonOrder Id</param>
        /// <returns></returns>
        // GET: api/GrabOrder/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            var order = hostelContext.PersonOrders.FirstOrDefault(d => d.Id == id);
            if (order == null)
            {
                return new { state = false, message = "无效的订单", code = 40025 };
            }
            var grabCount = hostelContext.GrabOrders.Count(d => d.POrderId == order.Id);
            if (grabCount <= 0)
            {
                for (int i = 0; i < order.ApplyNum; i++)
                {
                    hostelContext.GrabOrders.Add(new HostelModel.GrabOrderModel()
                    {

                        OrderId = order.OrderId,
                        PersonId = order.PersonId,
                        POrderId = order.Id,
                        RommStatus = 0,
                        RoomID = $"{order.CreateTime.ToString("yyyyMMdd")}_{id}_{order.PersonId}_{i}"
                    });

                }
                try
                {
                    hostelContext.SaveChanges();
                }
                catch (Exception)
                {

                    return new { state = false, message = "服务器错误,生成房间编号失败！", code = 50025 };
                }

            }


            return hostelContext.GrabOrders.Where(d => d.POrderId == order.Id).OrderBy(d => d.RoomID);
        }

        /// <summary>
        /// 服务人员新增 房间打扫
        /// </summary>
        /// <param name="model">GrabOrderModel</param>
        /// <returns>操作状态</returns>
        // POST: api/GrabOrder
        [HttpPost]
        public object Post([FromBody]GrabOrderModel model)
        {
            hostelContext.GrabOrders.Add(new HostelModel.GrabOrderModel()
            {

                OrderId = model.OrderId,
                PersonId = model.PersonId,
                POrderId = model.Id,
                RommStatus = 0,
                RoomID = model.RoomID
            });

            try
            {
                hostelContext.SaveChanges();
                return new { state = true, message = "操作成功", code = 200 };
            }
            catch (Exception)
            {

                return new { state = false, message = "保存失败,请核实数据是否完整,新增Room失败", code = 500225 };
            }
        }

        /// <summary>
        /// 服务人员更新房间状态
        /// </summary>
        /// <param name="id">PersonOrder Id</param>
        /// <param name="modes">房间状态集合字段</param>
        /// <returns>操作状态</returns>
        // PUT: api/GrabOrder/5
        [HttpPut("{id}")]
        public object Put(int id, [FromBody]IEnumerable<GrabOrderViewModel> modes)
        {
            var list = hostelContext.GrabOrders.Where(d => d.POrderId == id);
            var listUpdate = modes.OrderBy(d => d.Id).ToList();
         
            foreach (var item in list)
            {
                var obj = listUpdate.FirstOrDefault(d => d.Id == item.Id);
                if (obj != null)
                {
                    item.RommStatus = obj.RommStatus;
                }
              
            }
            try
            {
                hostelContext.SaveChanges();
                return new { state = true, message = "操作成功", code = 200 };
            }
            catch (Exception)
            {

                return new { state = false, message = "更新失败,请核实数据是否完整,新增Room失败", code = 500225 };
            }
        }


        /// <summary>
        /// 服务人员删除房间
        /// </summary>
        /// <param name="id">GrabID</param>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            var obj = hostelContext.GrabOrders.Find(id);
            if (obj == null)
            {
                return new { state = false, message = "无效的数据ID", code = 308 };
            }
            try
            {
                hostelContext.GrabOrders.Remove(obj);
                hostelContext.SaveChanges();
                return new { state = true, message = "操作成功", code = 200 };
            }
            catch (Exception)
            {

                return new { state = false, message = "保存失败,请核实数据是否完整,新增Room失败", code = 500225 };
            }
        }
    }
}
