using System;
using System.Diagnostics;
using System.Windows.Forms;
using DisplayCadFile.Properties;

public partial class Footer : UserControl
{
    public Footer()
    {
        InitializeComponent();
    }

    private void btnProductInformation_Click(object sender, EventArgs e)
    {
        Process.Start("http://gis.thinkgeo.com/Products/GISComponentsforNETDevelopers/MapSuiteServicesEdition/tabid/628/Default.aspx");
    }

    private void btnProductInformation_Activate(object sender, EventArgs e)
    {
        btnProductInformation.Image = Resources.btn_active_map_suite_products;
    }

    private void btnProductInformation_Deactivate(object sender, EventArgs e)
    {
        btnProductInformation.Image = Resources.btn_inactive_map_suite_products;
    }

    private void btnSupportCenter_Click(object sender, EventArgs e)
    {
        Process.Start("http://gis.thinkgeo.com/supportcenter");
    }

    private void btnSupportCenter_Activate(object sender, EventArgs e)
    {
        btnSupportCenter.Image = Resources.btn_active_support_center;
    }

    private void btnSupportCenter_Deactivate(object sender, EventArgs e)
    {
        btnSupportCenter.Image = Resources.btn_inactive_support_center;
    }

    private void btnDiscussForum_Click(object sender, EventArgs e)
    {
        Process.Start("http://gis.thinkgeo.com/Support/DiscussionForums/tabid/143/afv/topicsview/aff/11/Default.aspx");
    }

    private void btnDiscussForum_Activate(object sender, EventArgs e)
    {
        btnDiscussForum.Image = Resources.btn_active_discussion_forums;
    }

    private void btnDiscussForum_Deactivate(object sender, EventArgs e)
    {
        btnDiscussForum.Image = Resources.btn_inactive_discussion_forums;
    }

    private void btnWiki_Click(object sender, EventArgs e)
    {
        Process.Start("http://wiki.thinkgeo.com/wiki/Map_Suite_Services_Edition");
    }

    private void btnWiki_Activate(object sender, EventArgs e)
    {
        btnWiki.Image = Resources.btn_active_thinkgeo_wiki;
    }

    private void btnWiki_Deactivate(object sender, EventArgs e)
    {
        btnWiki.Image = Resources.btn_inactive_thinkgeo_wiki;
    }

    private void btnContactUs_Click(object sender, EventArgs e)
    {
        Process.Start("http://gis.thinkgeo.com/Default.aspx?tabid=147");
    }

    private void btnContactUs_Activate(object sender, EventArgs e)
    {
        btnContactUs.Image = Resources.btn_active_contact_us;
    }

    private void btnContactUs_Deactivate(object sender, EventArgs e)
    {
        btnContactUs.Image = Resources.btn_inactive_contact_us;
    }
}
