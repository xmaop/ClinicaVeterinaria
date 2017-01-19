<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucwFecha.ascx.cs" Inherits="ucwFecha" %>
<script type="text/javascript" language="javascript">
    function ValidarFecha(obj, id) {
        var fecha = obj.value;
        //var esBisiesto = true;
        if (fecha != '') {
            var fecha = fecha.split("/");
            var dia = fecha[0];
            var mes = fecha[1];
            var anio = fecha[2];
            var validacion = '';
            var elMes = parseInt(mes, 10);

            if (dia == '00') {
                validacion = '1';
            }

            if (anio < 1973 || anio > 9999) {
                validacion = '1';
            }
            if (elMes > 12 || elMes <= 0) {
                validacion = '1';
            }
            // MES FEBRERO 
            if (elMes == 2) {
                if (esBisiesto(anio)) {
                    if (parseInt(dia, 10) > 29) {
                        validacion = '1';
                    }
                    else
                        return true;
                }
                else {
                    if (parseInt(dia, 10) > 28) {
                        validacion = '1';
                    }
                    else
                        return true;
                }
            }

            //RESTO DE MESES 
            if (elMes == 4 || elMes == 6 || elMes == 9 || elMes == 11) {
                if (parseInt(dia, 10) > 30) {
                    validacion = '1';
                }
            }



            if (validacion == '1') {
                document.getElementById(id).value = "";
                alert("Fecha no válida");
                document.getElementById(id).value = "";
                obj.value = "";
                return false;
            }
        }
    }

    function esBisiesto(anio) {
        if ((anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) {
            return true;
        }
        else {
            return false;
        }
    }



</script>
<asp:TextBox ID="txtFechaVisita" runat="server" Width="70px" CssClass="textbox" 
    MaxLength="10" ontextchanged="txtFechaVisita_TextChanged"></asp:TextBox>
<cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaVisita"
    Format="dd/MM/yyyy">
</cc1:CalendarExtender>
<cc1:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
    Enabled="true" runat="server" TargetControlID="txtFechaVisita" CultureName="es-PE">
</cc1:MaskedEditExtender>
<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtFechaVisita"
    Display="None" ErrorMessage="rfv1"></asp:RequiredFieldValidator>
<asp:RangeValidator ID ="rvDate" runat ="server" ControlToValidate="txtFechaVisita" ErrorMessage="*" ForeColor="Red" Type="Date" 
 MinimumValue="01/01/1900" MaximumValue="01/01/2100" Display="Dynamic" Enabled="false"></asp:RangeValidator>
<asp:HiddenField runat="server" ID="hdfIndicaValidacionExterna" Value="N" />

