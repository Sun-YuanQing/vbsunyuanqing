using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Text;
public partial class Control_Pager : System.Web.UI.UserControl
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageBind();
        }
    }
   
    //传递进来的页码
    private int _PageIndex=1;
    private int _RecordCount;
    private int _PageSize=10;
    private int _PageCount;
    private string _w="";//跟随参数
    
    //计算之后的页码
    private int _PrePage;
    private int _NextPage;

    /// <summary>
    /// 当前页
    /// </summary>
    public int PageIndex
    {
        set { _PageIndex = value; }
        get { return _PageIndex; }
    }
    /// <summary>
    /// 总记录数
    /// </summary>
    public int RecordCount
    {
        set { _RecordCount = value; }
        get { return _RecordCount; }
    }
    /// <summary>
    /// 总记录数
    /// </summary>
    public int PageSize
    {
        set { _PageSize = value; }
        get { return _PageSize; }
    }
    /// <summary>
    /// 总页数
    /// </summary>
    public int PageCount
    {
        set { _PageCount = value; }
        get { return _PageCount; }
    }
    /// <summary>
    /// 跟随参数
    /// </summary>
    public string w
    {
        set { _w = value; }
        get { return _w; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void PageBind()
    {
        try
        {
            PageIndex = Convert.ToInt32(Request.QueryString["p"]);
        }
        catch
        {
            PageIndex = 1;
        }
        if (PageIndex < 1)
        {
            PageIndex = 1;
        }
        _PrePage = PageIndex - 1;
        _NextPage = PageIndex + 1;

        if (_RecordCount % _PageSize == 0)
        {
            _PageCount = _RecordCount / _PageSize;
        }
        else
        {
            _PageCount = _RecordCount / _PageSize + 1;
        }
        if (_PrePage < 1)
        {
            _PrePage = 1;
        }
        if (_NextPage > _PageCount)
        {
            _NextPage = _PageCount;
        }

        
         
          #region
        int PageListSize = 11;
        string _item = "";
        if (_PageCount <= PageListSize)
        {
            //总页数不够pagelistsize
            for (int i = 1; i < _PageCount+1; i++)
            {
                if (string.IsNullOrEmpty(_w))
                {
                    _item += string.Format("&nbsp;<a href=\"?p={0}\">{0}</a>&nbsp;", i);
                }
                else
                {
                    _item += string.Format("&nbsp;<a href=\"{0}&p={1}\">{1}</a>&nbsp;", _w, i);
                }
            }
        }
        else
        {
            if ((PageIndex>(PageListSize/2)) && ((PageCount-PageIndex)>(PageListSize/2)))
            {
                //前后都充足
                for (int i = (PageIndex - (PageListSize / 2)); i < (PageIndex + (PageListSize / 2)); i++)
                {
                    if (string.IsNullOrEmpty(_w))
                    {
                        _item += string.Format("&nbsp;<a href=\"?p={0}\">{0}</a>&nbsp;", i);
                    }
                    else
                    {
                        _item += string.Format("&nbsp;<a href=\"{0}&p={1}\">{1}</a>&nbsp;", _w, i);
                    }
                }

            }
            else if ((PageIndex > PageListSize / 2) && ((PageCount - PageIndex) <= PageListSize / 2))
            {
                //前面充足
                //前后不充足
                for (int i = PageCount-PageListSize; i <= PageCount; i++)
                {
                    if (string.IsNullOrEmpty(_w))
                    {
                        _item += string.Format("&nbsp;<a href=\"?p={0}\">{0}</a>&nbsp;", i);
                    }
                    else
                    {
                        _item += string.Format("&nbsp;<a href=\"{0}&p={1}\">{1}</a>&nbsp;", _w, i);
                    }
                    
                }
            }
            else
            {
                //后面充足 前面不充足
                for (int i = 1; i < PageListSize; i++)
                {
                    if (string.IsNullOrEmpty(_w))
                    {
                        _item += string.Format("&nbsp;<a href=\"?p={0}\">{0}</a>&nbsp;", i);
                    }
                    else
                    {
                        _item += string.Format("&nbsp;<a href=\"{0}&p={1}\">{1}</a>&nbsp;", _w, i);
                    }
                }
            }

        }
        # endregion 
        /**/

       

        
        string pagerstr = "";
        if (string.IsNullOrEmpty(_w))
        {
            pagerstr = string.Format("总共 {0} 条记录 每页{1}条 共 {2} 页 当前第 {3} 页    <a href=\"?p=1\">首页</a> <a href=\"?p={4}\">上一页</a> {6}<a href=\"?p={5}\">下一页</a> <a href=\"?p={2}\">尾页</a>", RecordCount, _PageSize, _PageCount, _PageIndex, _PrePage, _NextPage, _item);
        }
        else
        {
            pagerstr = string.Format("总共 {0} 条记录 每页{1}条 共 {2} 页 当前第 {3} 页    <a href=\"{6}&p=1\">首页</a> <a href=\"{6}&p={4}\">上一页</a> {7} <a href=\"{6}&p={5}\">下一页</a> <a href=\"{6}&p={2}\">尾页</a> ", RecordCount, _PageSize, _PageCount, _PageIndex, _PrePage, _NextPage, _w, _item);
        }
        this.ltrPager.Text = pagerstr;
    }
}
