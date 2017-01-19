<%@ Page Title="Servicio de Hospedaje" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ServicioHospedaje.aspx.cs" Inherits="WebPetCenter.ServicioHospedaje" %>




<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <link href="Content/jquery.alerts.css" rel="stylesheet" type="text/css" media="screen" />

    <link href="Content/Site.css" rel="stylesheet" />

    <asp:UpdatePanel ID="udpPrincipal" runat="server" ></asp:UpdatePanel>
    <asp:HiddenField ID="hdfIdSecuencia" runat="server" />
    <asp:HiddenField ID="hdf_TipoAlimento" runat="server" />
    <asp:HiddenField ID="hdfIdExpediente" runat="server" Value="0" />
    <asp:HiddenField ID="hdfIdPlan" runat="server" Value="0" />
    <asp:HiddenField ID="hdnIdObjetivoPlan" runat="server" Value="0" />


    <H2>Servicio de Hospedaje</H2>
        <div class="jumbotron">
            <fieldset>
                <legend>Reserva:</legend>
                        <div class="form-inline"> 
                            <label for="InputExpediente">Codigo Reserva:</label>   
                                <input type="text" runat="server" class="form-control" id="InputReserva" placeholder="Codigo Reserva" required="required" />
                                <asp:ImageButton ID="btnBuscar" runat="server" Height="20px" ImageAlign="Baseline" ImageUrl="~/Image/Buscar.png" OnClick="btnBuscar_Click" Width="20px" ToolTip="Buscar" />
                                <label for="InputFechaInicio">Fecha Entrada:</label>
                                <input type="text" class="form-control" runat="server" id="InputFechaInicioReserva" disabled/>
                                <label for="InputFechaSalida">Fecha Salida:</label>
                                <input type="text" class="form-control" runat="server" id="InputFechaSalidaReserva" disabled  />
            
                        </div>
            </fieldset>
        </div>
        <div class="jumbotron">
            <fieldset>
                <legend>Mascota:</legend>
                        <div class="form-inline">
                        <div class="form-group">
                                <label for="InputIdMascota">Id Mascota:</label>
                                <input type="text" class="form-control" runat="server" id="InputIdMascota" disabled/>
                            <label for="InputNombre">Nombre</label>
                                <input type="text" class="form-control" id="InputNombre" runat="server" disabled />
                            <label for="InputEspecie">Especie:</label>
                                <input type="text" class="form-control" runat="server" id="InputEspecie" disabled  />
           
                            <label for="InputRaza">Raza</label>
                                <input type="text" class="form-control" id="InputRaza" runat="server" disabled />
                                  </br>
                            <label for="InputEdad">Edad</label>
                                <input type="text" class="form-control" id="InputEdad" runat="server" disabled />
                            <label for="InputSexo">Sexo</label>
                                <input type="text" class="form-control" id="InputSexo" runat="server" disabled />
                            <label for="InputPeso">Peso</label>
                                <input type="text" class="form-control" id="InputPeso" runat="server" disabled />
                  
                        </div>
                        <div class="form-group">
                  <asp:Image ID="ImgFotografia" runat="server" Height="87px" ImageAlign="Middle"  Width="95px" BorderStyle="Groove" ViewStateMode="Enabled" BorderColor="#0066FF" BorderWidth="1px" />
                        
                        </div>
                        </div>

            </fieldset>
        </div>
    
    
        <div class="jumbotron">
            <fieldset>
                <legend>Cliente:</legend>
                       <div class="form-inline">
                            <label for="InputDNI">DNI</label>
                              <input type="text" class="form-control" id="InputDNI" runat="server" disabled />
                            <label for="InputClienteNombre">Nombre:</label>
                              <input type="text" class="form-control-large" runat="server"  id="InputClienteNombre" disabled  />
                        </div>
            </fieldset>
        </div>

        <div class="jumbotron">
            <fieldset>
                <legend>Revisión Médica:</legend>
                    <div class="form-inline">
                        <label for="InputRevision">Revisión Médica:</label>   
                            <input type="text" runat="server" class="form-control" id="InputRevision" placeholder="Revisión médica" required="required" />
                            <asp:ImageButton ID="imgBustarRM" runat="server" Height="20px" ImageAlign="Baseline" ImageUrl="~/Image/Buscar.png" OnClick="btnBuscar_Click" Width="20px" ToolTip="Buscar" />
                            <label for="InputFechaRM">Fecha:</label>
                            <input type="text" class="form-control" runat="server" id="InputFechaRM" disabled/>
                            </br>
                        <label for="InputObservacion">Observación:</label>
                            <textarea class="form-control" runat="server" id="InputObservacion" disabled/>
                            <label for="InputResultado">Resultados:</label>
                            <textarea class="form-control" runat="server" id="InputResultado" disabled/>
             
                    </div>
            </fieldset>
        </div>

    
        <div class="jumbotron">
            <fieldset>
                <legend>Servicio Hospedaje:</legend>
                    <div class="form-inline">
                        <label for="InputFechaInicioSH">Fecha Entrada:</label>
                        <asp:TextBox ID="InputFechaInicioSH" runat="server" Width="120px" Height="20px" Font-Size="12px" TextMode="Date"></asp:TextBox>
                        <asp:TextBox ID="InputFechaInicioHoraSH" runat="server" Width="90px" Height="20px" Font-Size="12px" TextMode="Time"></asp:TextBox>
                        <label for="InputFechaSalidaSH">Fecha Salida:</label>
                        <asp:TextBox ID="InputFechaSalidaSH" runat="server" Width="120px" Height="20px" Font-Size="12px" TextMode="Date"></asp:TextBox>
                        <asp:TextBox ID="InputFechaSalidaHoraSH" runat="server" Width="90px" Height="20px" Font-Size="12px" TextMode="Time"></asp:TextBox>
             
                    </div>
                    <div class="form-inline">
                        <label for="InputFechaInicioSH">Estado:</label>
                            <select class="form-control" id="Select1" runat="server" name="Estado" >
                            </select>
                        <label for="InputObservacionSH">Observación:</label>
                            <textarea class="form-control" runat="server" id="InputObservacionSH" />
             
                    </div>
            </fieldset>
        </div>
    
    <div class="jumbotron">
        <div class="form-inline text-center">
                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" />
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" OnClientClick="return confirm('Está seguro de Guardar el registro seleccionado ? '); "/>
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" OnClientClick="return confirm('Está seguro de eliminar el registro seleccionado ? ');"  />
         
        </div>
    </div>

</div>
</div>
</div>
    </div>

</asp:Content>
