using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Services;
using BS.Common.Dao.Sql;
using BS.Common.Entities.Page;

namespace BSFrameworkService
{
    /// <summary>
    /// Summary description for PageService
    /// </summary>
    [WebService(Namespace = "http://www.beltransoft.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PageService : System.Web.Services.WebService
    {
        private PageSqlDAO pageSqlDAO = new PageSqlDAO();
        JavaScriptSerializer ser = new JavaScriptSerializer();

        [WebMethod]
        public string GetPageConfig(string appName, string pageId, string pageName)
        {
            Page page = GetPageInfoDAO().GetPageConfig(appName, pageId, pageName);

            return Serialize(page);
        }

        [WebMethod]
        public void SavePage(string appName, string page)
        {
            GetPageInfoDAO().SavePage( appName, DeserializePage(page) );
        }

        [WebMethod]
        public void DeletePage(string page)
        {
            GetPageInfoDAO().DeletePage( DeserializePage(page) );
        }

        [WebMethod]
        public string GetPageList(string appName)
        {
            return Serialize( GetPageInfoDAO().GetPageList(appName) );
        }

        [WebMethod]
        public string GetPageListItems(string fieldName, string orderBy, string orderType)
        {
            List<PageListItem> list = (List<PageListItem>) GetPageInfoDAO().GetPageListItems(fieldName, orderBy, orderType);
            
            return Serialize(list);
        }

        private string Serialize(object obj)
        {
            ser.MaxJsonLength = int.MaxValue;            
            return ser.Serialize(obj);
        }

        private Page DeserializePage(string json)
        {
            return (Page) ser.Deserialize(json, typeof(Page));
        }

        private PageSqlDAO GetPageInfoDAO()
        {
            return pageSqlDAO;
        }
    }
}
