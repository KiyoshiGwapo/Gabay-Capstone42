<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DashBoard/Student_Homepage/Student_Master.Master" AutoEventWireup="true" CodeBehind="Appointment_Status.aspx.cs" Inherits="Gabay_Final_V2.Views.Modules.Appointment.Appointment_Status" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<h1 style="text-align: center; padding: 9px; border: 2px solid #333; background-color: #f4f4f4; color: #333; border-radius: 10px;">Appointment Status</h1>
	<style>
		.img-container{
			display:flex;
			justify-content:center;
			height: 200px;
			width:auto;
		}
		.appointmentLbl{
			text-align:end;
		}
		.appointmentHolder{
			margin-left:50px;
		}
		.appointmentLbls{
			margin: 20px 20px;
		}
		.appointment-Body{
			margin-top:20px;
			display: flex;
			flex-direction:column;
			align-items:center;
		}
		table{
			width: 600px !important;
		}
		table td{
			 vertical-align: middle;
		}
		 .custom-button {
			background-color: darkblue; 
			color: #fff;
			padding: 10px 20px; 
			border: none; 
			border-radius: 5px;
			cursor: pointer; 
			margin-top: 20px;
			float: right;
		}

		.custom-button:hover {
			 background-color: #007bff; 
		}
		.reschedBtn{
			margin-left:5px;
		}
		.reschedBtn:hover{
			opacity:75%;
		}
		.acceptBtn{
			width: 150px;
		}
	</style>
	<asp:Button ID="ViewHistoryButton" runat="server" Text="View My History" CssClass="custom-button" OnClick="ViewHistoryButton_Click" />
	<div class="container">
			<div class="row">
				<div class="col-12">
					<div class="Appointment-Status">
						<div class="img-container">
							<asp:Image ID="Image1" runat="server" />
						</div>
						<div class="d-flex justify-content-around text-center mt-3">
							<%--<asp:Label ID="StatusLabel" runat="server" Text="Pending" CssClass="fs-2"></asp:Label>--%>
							<span class="fs-5">
								<asp:Label ID="Indication" runat="server" Text="Label"></asp:Label>
							</span>
						</div>
						<div class="appointment-Body">
							<table class="table table-borderless">
								<tr class="appointmentLbls">
									<th class="appointmentLbl">
										<asp:Label ID="appointmentIDLbl" runat="server" Text="Appointment ID:" ></asp:Label>
									</th>
									<td>
										<asp:Label ID="appointmentID" runat="server" Text="Sample ID" CssClass="appointmentHolder"></asp:Label>
									</td>
								</tr>
								<tr class="appointmentLbls">
									<th class="appointmentLbl">
										<asp:Label ID="appointmentStatusLbl" runat="server" Text="Status:"></asp:Label>
									</th>
									<td>
										<asp:Label ID="appointmentStatus" runat="server" Text="Sample Status" CssClass="appointmentHolder"></asp:Label>
									</td>
								</tr>
								<tr class="appointmentLbls">
									<th class="appointmentLbl">
										<asp:Label ID="appointmentDateLbl" runat="server" Text="Date:"></asp:Label>
									</th>
									<td>
										<asp:Label ID="appointmentDate" runat="server" Text="Sample Date" CssClass="appointmentHolder fw-bold"></asp:Label>
									</td>
								</tr>
								<tr class="appointmentLbls">
									<th class="appointmentLbl">
										<asp:Label ID="appointmentTimeLbl" runat="server" Text="Time:"></asp:Label>
									</th>
									<td>
										<asp:Label ID="appointmentTime" runat="server" Text="Sample Time" CssClass="appointmentHolder fw-bold"></asp:Label>
									</td>
								</tr>
								<tr class="appointmentLbls">
									<th class="appointmentLbl">
										<asp:Label ID="appointmentConcernLbl" runat="server" Text="Concern:"></asp:Label>
									</th>
									<td>
										<asp:Label ID="appointmentConcern" runat="server" Text="Sample Concern" CssClass="appointmentHolder">
										</asp:Label>
									</td>
								</tr>
							</table>
						</div>
						<div class="d-flex justify-content-center" runat="server" id="reschedBtns">
							<asp:Button ID="AcceptReschedBtn" runat="server" Text="Accept" CssClass="btn bg-success text-light reschedBtn acceptBtn" OnClick="AcceptReschedBtn_Click"/>
							<asp:Button ID="RejectReschedBtn" runat="server" Text="Reject" CssClass="btn bg-danger text-light reschedBtn" OnClick="RejectReschedBtn_Click" />
						</div>
					</div>
				</div>
			</div>
		</div>
	<%-- Reject Modal --%>
	<div class="modal fade" id="rejectModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
		<div class="modal-dialog modal-dialog-centered">
			<div class="modal-content">
				<div class="modal-header">
					<asp:Button ID="rejectAppmntCls" runat="server" CssClass="btn-close" OnClick="rejectAppmntCls_Click"/>
				</div>
				<div class="modal-body">
					<p>Are you sure to reject this appoint?</p>
					<span>Rejecting this appointment means your appointment ticket will be closed</span>
				</div>
				<div class="modal-footer">
					<asp:Button ID="cancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="cancel_Click"/>
					<asp:Button ID="rejectAppmntBtn" runat="server" Text="Proceed" CssClass="btn btn-primary text-light" OnClick="rejectAppmntBtn_Click" />
				</div>
			</div>
		</div>
	</div>
	<%-- Success modal --%>
	<div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
		<div class="modal-dialog modal-dialog-centered">
			<div class="modal-content">
				<div class="modal-body bg-success text-center text-light">
					<i class="bi bi-info-circle-fill"></i>
					<p id="successMessage"></p>
				</div>
			</div>
		</div>
	</div>
	<%-- Error modal --%>
	<div class="modal fade" id="errorModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
		<div class="modal-dialog modal-dialog-centered">
			<div class="modal-content">
				<div class="modal-body bg-danger text-center text-light">
					<i class="bi bi-exclamation-circle-fill"></i>
					<p id="errorMessage"></p>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
