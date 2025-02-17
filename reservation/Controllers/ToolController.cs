using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using SqlSugar;
using System.Reflection;

namespace webapi.Controllers
{
    public class ToolController : BaseController
    {
        private readonly ISqlSugarClient _db;
        public ToolController(ISqlSugarClient db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<bool> CodeFirst()
        {
            //1. create DB
            _db.DbMaintenance.CreateDatabase();
            //2. By reflection, load the assembly set, read all types, and then create tables according to the entity
            string nspace = "Model.Entitys";
            Type[] ass= Assembly.LoadFrom(AppContext.BaseDirectory+"/Model.dll").GetTypes().Where(p => p.Namespace ==nspace).ToArray();
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
            //3. Initialize super administrator and menu
            Users user = new Users()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NickName = "super admin",
                Password = "123456",
                UserType = 0,
                IsEnable = true,
                Description = "default super administrator when initialization of the database",
                CreateDate = DateTime.Now,
                CreateUserId = "",
            };
            string userId = (await _db.Insertable(user).ExecuteReturnEntityAsync()).Id;
            var m1 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Menu Management",
                Index = "/menu",
                FilePath = "menu.vue",
                ParentId = "",
                Order = 1,
                IsEnable = true,
                Icon = "folder",
                Description = "default menu when initialization of the database",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            string mid1 = (await _db.Insertable(m1).ExecuteReturnEntityAsync()).Id;
            var ml1 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Menu List",
                Index = "/menu",
                FilePath = "menu.vue",
                ParentId = mid1,
                Order = 1,
                IsEnable = true,
                Icon = "notebook",
                Description = "default menu when initialization of the database",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            await _db.Insertable(ml1).ExecuteReturnEntityAsync();
            var m2 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Role Management",
                Index = "/role",
                FilePath = "role.vue",
                ParentId = "",
                Order = 1,
                IsEnable = true,
                Icon = "folder",
                Description = "default menu when initialization of the database",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            string mid2 = (await _db.Insertable(m2).ExecuteReturnEntityAsync()).Id;
            var m22 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Role List",
                Index = "/role",
                FilePath = "role.vue",
                ParentId = mid2,
                Order = 1,
                IsEnable = true,
                Icon = "notebook",
                Description = "default menu when initialization of the database",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            await _db.Insertable(m22).ExecuteReturnEntityAsync();

            var m3 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User Management",
                Index = "/user",
                FilePath = "user.vue",
                ParentId = "",
                Order = 1,
                IsEnable = true,
                Icon = "folder",
                Description = "default menu when initialization of the database",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            string mid3 = (await _db.Insertable(m3).ExecuteReturnEntityAsync()).Id;
            var m33 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User List",
                Index = "/user",
                FilePath = "user.vue",
                ParentId = mid3,
                Order = 1,
                IsEnable = true,
                Icon = "notebook",
                Description = "default menu when initialization of the database",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            return await _db.Insertable(m33).ExecuteCommandIdentityIntoEntityAsync();
        }
    }
}
