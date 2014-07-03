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
        //const string frstname = "First Name";
        //const string lstname = "Last Name";
        //const string position = "Position";
        //const string height = "Height";
        //const string weight = " Weight";
        //const string city = " City";
        //const string state = "State";
        //System.Type typestring = typeof(System.String);

        Player players = new USAB.BLL.Player();
        IList<PlayerInfo> getplayers = players.getPlayers();

        PlayerInfo getplayersbyid = players.getPlayer(1);

        //DataTable dt = new DataTable();
        //DataColumn column;
        //DataRow row;

        //column = new DataColumn(frstname, typestring);
        //dt.Columns.Add(column);

        //column = new DataColumn(lstname, typestring);
        //dt.Columns.Add(column);

        //column = new DataColumn(position, typestring);
        //dt.Columns.Add(column);

        //column = new DataColumn(height, typestring);
        //dt.Columns.Add(column);

        //column = new DataColumn(weight, typestring);
        //dt.Columns.Add(column);

        //column = new DataColumn(city, typestring);
        //dt.Columns.Add(column);

        //column = new DataColumn(state, typestring);
        //dt.Columns.Add(column);

        //foreach (PlayerInfo getply in getplayers)
        //{
        //    row = dt.NewRow();
        //    row[frstname] = getply.firstname;
        //    row[lstname] = getply.lastname;
        //    row[position] = getply.position;
        //    row[weight] = getply.weight;
        //    row[city] = getply.city;
        //    row[state] = getply.state;
        //    dt.Rows.Add(row);

        //}

        //foreach (DataColumn col in dt.Columns)
        //{
        //    BoundField bfield = new BoundField();

        //    bfield.DataField = col.ColumnName;

        //    bfield.HeaderText = col.ColumnName;

        //    bfield.SortExpression = col.ColumnName;

        //    GridView1.Columns.Add(bfield);
        //}

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