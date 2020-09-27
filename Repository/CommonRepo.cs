using DynamicData.Data;
using DynamicData.Interface;
using DynamicData.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicData.Repository
{
    public class CommonRepo : ICommon
    {
        private readonly DatabaseContext _context;
        private readonly ILibrary _iLibrary;
        public CommonRepo(DatabaseContext dbContext, ILibrary ilibrary)
        {
            _context = dbContext;
            _iLibrary = ilibrary;
        }

        public string BreadCrumbs(Guid Guid)
        {
            StringBuilder pageBC = new StringBuilder();
            return pageBC.ToString();
        }

        public List<Dictionary<string, object>> SPReader(string storeProcedure)
        {
            var data = new List<Dictionary<string, object>>();
            DbConnection conn = _context.Database.GetDbConnection();

            DbCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = storeProcedure;

            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                            row[reader.GetName(i).ToLower()] = reader[i];
                        data.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
            return data;
        }

        public bool SPNonQuery(string storeProcedure)
        {
            DbConnection conn = _context.Database.GetDbConnection();

            DbCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = storeProcedure;

            try
            {
                var result = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (conn.State.Equals(ConnectionState.Open)) { conn.Close(); }
            }
        }

        public List<Models.Library> LibraryHierarchy()
        {
            List<Models.Library> Hierarchy = new List<Models.Library>();
            var libraries = _iLibrary.LibraryCollection().Result.ToList();
            foreach (var item in libraries)
            {
                Models.Library lib = new Models.Library();
                lib.GUID = item.GUID;
                lib.ID = item.ID;
                lib.Title = GetParentsString(libraries, item) + item.Title;
                Hierarchy.Add(lib);
            }
            return Hierarchy;
        }

        public string GetParentsString(List<Library> Libraries, Library current)
        {
            string path = "";
            Action<List<Library>, Library> GetPath = null;
            GetPath = (List<Library> ps, Library p) =>
            {
                var parents = Libraries.Where(x => x.ID == p.ParentID);
                foreach (var parent in parents)
                {
                    path += $"/{ parent.Name}";
                    GetPath(ps, parent);
                }
            };
            GetPath(Libraries, current);
            string[] split = path.Split(new char[] { '/' });
            Array.Reverse(split);
            return string.Join("/", split);
        }

        public List<Dictionary<string, object>> ListStates()
        {
            return SPReader("spGetAllStates");
        }

        public async Task<List<Role>> RoleCollection(bool sysAdmin)
        {
            if (sysAdmin)
                return await _context.Role.ToListAsync();
            else
                return await _context.Role.Where(w => w.Name != "SYSADMIN").ToListAsync();
        }


    }
}
