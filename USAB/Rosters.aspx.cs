using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using USAB.Model;
using USAB.BLL;


public partial class Rosters : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Team"] != null)
            {
                switch (Request.QueryString["Team"])
                {
                    case "Elite":
                        populateRosterGridView("Elite");
                        break;
                    case "Open":
                        populateRosterGridView("Open");
                        break;
                    case "U18":
                        populateRosterGridView("U18");
                        break;
                    case "Senior":
                        populateRosterGridView("Senior");
                        break;

                }
            }
        }
    }

    private void populateRosterGridView(string team)
    {

        Player players = new USAB.BLL.Player();
        IList<PlayerInfo> getplayers = players.getPlayers();
        PlayerInfo getplayersbyid = players.getPlayer(1);
        DataTable getDT = players.getAllPlayers();

        GridView1.DataSource = getDT;
        GridView1.DataBind();

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (DataControlFieldCell rw in row.Cells)
            {
                rw.HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}