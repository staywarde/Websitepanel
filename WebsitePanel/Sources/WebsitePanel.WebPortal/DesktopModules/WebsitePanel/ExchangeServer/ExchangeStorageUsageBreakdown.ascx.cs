// Copyright (c) 2011, Outercurve Foundation.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
// - Redistributions of source code must  retain  the  above copyright notice, this
//   list of conditions and the following disclaimer.
//
// - Redistributions in binary form  must  reproduce the  above  copyright  notice,
//   this list of conditions  and  the  following  disclaimer in  the documentation
//   and/or other materials provided with the distribution.
//
// - Neither  the  name  of  the  Outercurve Foundation  nor   the   names  of  its
//   contributors may be used to endorse or  promote  products  derived  from  this
//   software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,  BUT  NOT  LIMITED TO, THE IMPLIED
// WARRANTIES  OF  MERCHANTABILITY   AND  FITNESS  FOR  A  PARTICULAR  PURPOSE  ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL,  SPECIAL,  EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO,  PROCUREMENT  OF  SUBSTITUTE  GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)  HOWEVER  CAUSED AND ON
// ANY  THEORY  OF  LIABILITY,  WHETHER  IN  CONTRACT,  STRICT  LIABILITY,  OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE)  ARISING  IN  ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using WebsitePanel.Providers.HostedSolution;

namespace WebsitePanel.Portal.ExchangeServer
{
	public partial class ExchangeStorageUsageBreakdown : WebsitePanelModuleBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindStatistics();
			}
		}

		private void BindStatistics()
		{
			// total counters
			int totalMailboxItems = 0;
			int totalMailboxesSizeMB = 0;
			int totalFolderItems = 0;
			int totalFoldersSizeMB = 0;

			// mailboxes
			ExchangeItemStatistics[] mailboxes = ES.Services.ExchangeServer.GetMailboxesStatistics(PanelRequest.ItemID);
			gvMailboxes.DataSource = mailboxes;
			gvMailboxes.DataBind();

			foreach (ExchangeItemStatistics item in mailboxes)
			{
				totalMailboxItems += item.TotalItems;
				totalMailboxesSizeMB += item.TotalSizeMB;
			}

			lblTotalMailboxItems.Text = totalMailboxItems.ToString();
			lblTotalMailboxSize.Text = totalMailboxesSizeMB.ToString();



			// public folders
			ExchangeItemStatistics[] folders = ES.Services.ExchangeServer.GetPublicFoldersStatistics(PanelRequest.ItemID);
			gvFolders.DataSource = folders;
			gvFolders.DataBind();

			foreach (ExchangeItemStatistics item in folders)
			{
				totalFolderItems += item.TotalItems;
				totalFoldersSizeMB += item.TotalSizeMB;
			}

			lblTotalFolderItems.Text = totalFolderItems.ToString();
			lblTotalFolderSize.Text = totalFoldersSizeMB.ToString();
		}
	}
}