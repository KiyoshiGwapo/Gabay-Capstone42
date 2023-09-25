﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Gabay_Final_V2.Models;

namespace Gabay_Final_V2.Views.Modules.CampusInfo
{
    public partial class Student_CampusInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Create an instance of the CampusInfo_model class
                CampusInfo_model campusInfoModel = new CampusInfo_model();

                // Call the GetCampusInformation method to retrieve campus information
                DataTable campusInfoData = campusInfoModel.GetCampusInformation();

                // Bind the retrieved data to your Repeater (rptAccordion)
                rptAccordion.DataSource = campusInfoData;
                rptAccordion.DataBind();
            }
        }


        private void BindAccordionData()
        {
            CampusInfo_model campusInfoModel = new CampusInfo_model();
            DataTable campusInfoData = campusInfoModel.GetCampusInformation();

            rptAccordion.DataSource = campusInfoData;
            rptAccordion.DataBind();
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (int.TryParse(hdnAccordionIndex.Value, out int campusInfoId))
            {
                string editedTitle = txtNewTitle.Text;
                string editedContent = txtNewContent.Text;

                CampusInfo_model campusInfoModel = new CampusInfo_model();
                bool updateResult = campusInfoModel.UpdateCampusInformation(campusInfoId, editedTitle, editedContent);

                if (updateResult)
                {
                    // Content updated successfully
                    Response.Write("<script>alert('Content updated successfully.');</script>");
                }
                else
                {
                    // Content update failed
                    Response.Write("<script>alert('Content update failed. Please try again later.');</script>");
                }
            }
        }

        protected void btnDeleteContent_Click(object sender, EventArgs e)
        {
            if (int.TryParse(hdnAccordionIndex.Value, out int campusInfoId))
            {
                CampusInfo_model campusInfoModel = new CampusInfo_model();
                bool deleteResult = campusInfoModel.DeleteCampusInformation(campusInfoId);

                if (deleteResult)
                {
                    // Content deleted successfully
                    Response.Write("<script>alert('Content deleted successfully.');</script>");
                }
                else
                {
                    // Content deletion failed
                    Response.Write("<script>alert('Content deletion failed. Please try again later.');</script>");
                }

                // Refresh the page to update the accordion with the modified content
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnAddNewRecord_Click(object sender, EventArgs e)
        {
            string newTitle = txtNewTitle.Text;
            string newContent = txtNewContent.Text;

            CampusInfo_model campusInfoModel = new CampusInfo_model();
            bool saveResult = campusInfoModel.SaveCampusInformation(newTitle, newContent);

            if (saveResult)
            {
                // New record added successfully
                Response.Write("<script>alert('New record added successfully.');</script>");
            }
            else
            {
                // New record insertion failed
                Response.Write("<script>alert('Failed to add new record. Please try again later.');</script>");
            }

            // Refresh the page to update the accordion with the modified content
            Response.Redirect(Request.RawUrl);
        }

        protected void btnEditContent_Click(object sender, EventArgs e)
        {
            if (int.TryParse(hdnAccordionIndex.Value, out int campusInfoId))
            {
                string editedTitle = txtNewTitle.Text;
                string editedContent = txtNewContent.Text;

                CampusInfo_model campusInfoModel = new CampusInfo_model();
                bool updateResult = campusInfoModel.UpdateCampusInformation(campusInfoId, editedTitle, editedContent);

                if (updateResult)
                {
                    // Content updated successfully
                    Response.Write("<script>alert('Content updated successfully.');</script>");
                }
                else
                {
                    // Content update failed
                    Response.Write("<script>alert('Content update failed. Please try again later.');</script>");
                }

                // Refresh the page to update the accordion with the modified content
                Response.Redirect(Request.RawUrl);
            }
        }

    }
}