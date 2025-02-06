using Model.Dto.Role;
using Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IRoleService
    {
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> Add(RoleAdd req, string userId);

        /// <summary>
        /// 編集
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> Edit(RoleEdit req, string userId);

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Del(string id);

        /// <summary>
        /// 一括削除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> BatchDel(string ids);

        /// <summary>
        /// リスト取得
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<PageInfo<RoleRes>> GetRoles(RoleReq req, string userId);
    }
}
