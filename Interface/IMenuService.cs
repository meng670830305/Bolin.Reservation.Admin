using Model.Dto.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IMenuService
    {
        Task<bool> Add(MenuAdd req, string userId);
        Task<bool> Edit(MenuEdit req, string userId);
        Task<bool> Del(string userId);
        Task<bool> BatchDel(List<string> userIds);
        /// <summary>
        /// メニュー取得(権限付け)
        /// </summary>
        /// <param name="res"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<MenuRes>> Get(MenuReq req, string userId);
        /// <summary>
        /// メニュー設定
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="mids"></param>
        /// <returns></returns>
        Task<bool> Setting(string rid, string mids);

    }
}
