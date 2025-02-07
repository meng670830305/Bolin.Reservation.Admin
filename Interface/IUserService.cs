using Model.Dto.Login;
using Model.Dto.User;
using Model.Other;
using Npgsql.TypeHandlers.FullTextSearchHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IUserService
    {
        /// <summary>
        /// ユーザー情報取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<UserRes> GetUser(LoginReq req);

        /// <summary>
        /// 個人情報編集
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<bool> EditNickNameOrPassword(string userId, PersonEdit req);

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> Add(UserAdd req, string userId);

        /// <summary>
        /// 編集
        /// </summary>
        /// <param name="req"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> Edit(UserEdit req, string userId);

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
        Task<PageInfo<UserRes>> GetUsers(UserReq req, string userId);

        /// <summary>
        /// ロール設置
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        Task<bool> SettingRole(string uid, string rids);
    }
}
