var K_StrDeseaEliminar = "¿Desea eliminar el registro?";
var K_StrConfirmarGrabar = "¿Está seguro de grabar? ";

/**********************************************************
PROPOSITO	: Generalizar las ventanas emergentes.
LLAMADA		: .
AUTOR       : Hanz Castillo Meza    17/06/2009
**********************************************************/
function MostrarVentana(intAncho, intAlto, strUrl, strNombre) {
    var xpos = (screen.width / 2) - (intAncho / 2);
    var ypos = (screen.height / 2) - (intAlto / 2);
    window.open(strUrl, strNombre, 'top=' + ypos + ',left=' + xpos + ',location=no,width=' + intAncho + ',height=' + intAlto + ',toolbar=0,resizable=no, status=0, location=0, directories=0, menubar=0, scrollbars=1, resize=0');
}

//retorna al padre los valores y separados por '|'
function MostrarVentanaModalDialog(intAncho, intAlto, strUrl, strNombre) {
    //    var xpos = (screen.width / 2) - (intAncho / 2);
    //    var ypos = (screen.height / 2) - (intAlto / 2);
    var hora = new Date().getTime();
    amperstan = strUrl.indexOf("?");
    cad = "";
    if (amperstan != -1) { cad = "&"; }
    else { cad = "?"; }
    return showModalDialog(strUrl + cad + "aRnd=" + hora, strNombre, "dialogWidth:" + intAncho + "px;dialogHeight:" + intAlto + "px;center:yes; status=0");
    //return showModalDialog(strUrl, strNombre, 'top=' + ypos + ',left=' + xpos + ',location=no,width=' + intAncho + ',height=' + intAlto + ',toolbar=0,resizable=yes, status=0, location=0, directories=0, menubar=0, scrollbars=1, resize=0');
}


/**********************************************************
PROPOSITO	: Limpia los valores de los elementos input text 
y select de la pagina.
LLAMADA		: en el evento onclick del boton limpiar
AUTOR		: Hanz 14/01/2008
**********************************************************/
function FC_limpiarFiltros() {
    var tblTablaFiltros = document.getElementById('tblTablaFiltros');
    if (tblTablaFiltros != null) {
        var oInputs = tblTablaFiltros.getElementsByTagName('input')
        var oSelects = tblTablaFiltros.getElementsByTagName('select')

        for (i = 0; i < oInputs.length; i++) {
            if (oInputs[i].type == 'text') {
                oInputs[i].value = '';
            }
        }

        for (var x = 0; x < oSelects.length; x++) {
            for (var y = 0; y < oSelects[x].options.length; y++) {
                if (oSelects[x].options[y].value == -1) {
                    oSelects[x].options[y].selected = true;
                    break;
                }
            }
        }
    }
    return true;
}

/**********************************************************
PROPOSITO	: Valida numeros decimales negativos con determinado numero de enteros y decimales.                    
LLAMADA		: En las validaciones de las cajas de texto.
AUTOR		: GAM 24/11/2009
**********************************************************/
var textoAnt = '';

//Logica de Validacion Numerica
function IsNumeric(cajaTexto, aceNeg, nroEnt, nroDec) {
    var resultado = true;
    var texto = cajaTexto.value;
    var ini = texto.length - 1;
    var puntoIni = texto.indexOf('.');
    //
    var largo = ini + 1;
    var negaIni = texto.indexOf('-');

    var RE = /^-{0,1}\d*\.{0,1}\d+$/;
    resultado = RE.test(texto);

    if (!resultado) {
        if
       (
         texto == '' ||
         (texto == '-' && aceNeg) ||
         (
          ini > 0 && texto.substring(ini) == '.' && !isNaN(texto.substring(ini - 1, ini)) &&
          ini == puntoIni && nroDec != 0
         )

       )
            resultado = true;
    }
    if (resultado) {
        //Validacion de negativo final, complementa lo anterior
        if (texto.indexOf('-') == 0 && !aceNeg)
            resultado = false;

        //validacion de cantidad de enteros y decimales
        //Si es entero positivo
        if (puntoIni == -1 && nroEnt != -1 && negaIni == -1 && largo > nroEnt)
            resultado = false;
        //Si es entero negativo
        if (puntoIni == -1 && nroEnt != -1 && negaIni == 0 && largo - 1 > nroEnt)
            resultado = false;
        //Si es decimal
        if (puntoIni != -1 && nroDec != -1 && negaIni == -1 &&
        (texto.substring(0, puntoIni).length > nroEnt
           || texto.substring(puntoIni + 1).length > nroDec))
            resultado = false;
        if (puntoIni != -1 && nroDec != -1 && negaIni != -1 &&
        (texto.substring(1, puntoIni).length > nroEnt
           || texto.substring(puntoIni + 1).length > nroDec))
            resultado = false;
    }

    return resultado;
}

//Logica de Retroceso
function valorAnt(e, cajaTexto, aceNeg, nroEnt, nroDec) {
    var texto = cajaTexto.value;

    if (IsNumeric(cajaTexto, aceNeg, nroEnt, nroDec)) {
        textoAnt = texto;
    }

    //Logica para que no se vean las letras metidas
    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    return (key <= 12 || (key >= 48 && key <= 57) || key == 46 || key == 45);
}

function FP_SoloDecimalesNyP(cajaTexto, aceNeg, nroEnt, nroDec) {
    var texto = cajaTexto.value;

    if (!IsNumeric(cajaTexto, aceNeg, nroEnt, nroDec)) {
        cajaTexto.value = textoAnt;
    }
}

function trim(myString) {
    return myString.replace(/^\s+/g, '').replace(/\s+$/g, '');
}


function cambia(obj, ImgC) {
    var v = document.all(obj.id);
    obj.src = ImgC;
}

/***************************************************************************
PROPOSITO	: Valida el ingreso de valores alfanumericos con 
signo y separador de puntos.
LLAMADA		: e: event
AUTOR		: 
***************************************************************************/
function FP_SoloLetras(e) {
    var target = (e.target ? e.target : e.srcElement);
    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    return (key <= 12 || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key == 32) || (key >= 164 && key <= 165));
}
function FP_SoloLetrasyNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla == 8) return true; // 3
    if (tecla == 189) return true; // 3
    if (tecla == 109) return true; // 3
    if (tecla == 111) return true; // 3
    if (tecla == 13) return false; //
    patron = /[A-Za-z0-9ñÑ!¡#$%&()=?¿*+.,;´:|\[\]\{\}\""\/\\\-\_\''\s]/;
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}

function FP_CorreoElectronico(e) {
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla == 8) return true; // 3
    if (tecla == 189) return true; // 3
    if (tecla == 109) return true; // 3
    if (tecla == 111) return true; // 3
    if (tecla == 13) return false; //
    patron = /[A-Za-z0-9ñÑ.;@\-\_]/;
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}

function FP_SoloLetrasyNumerosLimitado(e) {
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla == 8) return true; // 3
    patron = /[A-Za-z0-9\s]/;  // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}
function FP_SoloLetrasyNumerosLimitadoConGuion(e) {
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla == 8) return true; // 3
    patron = /[A-Za-z0-9-]/;  // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}
function FP_SoloLetrasyNumerosLimitadoConGuionyPunto(e) {
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla == 8) return true; // 3
    patron = /[A-Za-z0-9ñÑ\-.\s]/;  // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}

function FP_SoloLetrasyNumerosSinEspacio(e) {
    var target = (e.target ? e.target : e.srcElement);
    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    return (key <= 12 || (key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 164 && key <= 165) || (key == 241) || (key == 209) || (key == 46));
}
function FP_SoloNumeros(e) {
    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    return (key <= 12 || (key >= 48 && key <= 57));
}
function FP_SoloLetrasyNumerosConSaltos(e) {
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla == 8) return true; // 3
    if (tecla == 189) return true; // 3
    if (tecla == 109) return true; // 3
    if (tecla == 111) return true; // 3
    if (tecla == 13) return true; // 3
    patron = /[A-Za-z0-9ñÑ!¡#$%&()=?¿*+.,;´:<>|\[\]\{\}\""\/\\\-\_\''\s]/;  // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
    //    var target = (e.target ? e.target : e.srcElement);
    //    var key = (e ? e.keyCode || e.which : window.event.keyCode);
    //    return (key <= 13 || (key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key == 32) || (key >= 164 && key <= 165) || (key == 241) || (key == 209) || (key == 46));
}

/**********************************************************
PROPOSITO	: Corta la cadena del control especificado a una
determinada longitud.
LLAMADA		: onKeyPress, onKeyUp
AUTOR       : Hanz Castillo Meza    05/08/2009
**********************************************************/
function cortarCaracteres(control, cant) {
    if (control.value.length > cant) {
        control.value = control.value.substring(0, cant);
    }
}


// Muestra mensaje de resultado de ocurencia 
// y redirecciona la pagina.
function FMensajeOcurrencia(mensaje, url) {
    alert(mensaje);
    window.location = url;
}

// Pregunta al usuario si desea mantener en la página o regresar a la anterior
// luego de un resultado de ocurrenci
function FMensajeDecisionOcurrencia(mensaje, urlSI, urlNO) {
    var opt = confirm(mensaje);
    if (opt) {
        window.location = urlSI;
    } else {
        window.location = urlNO;
    }
}

// Muestra mensaje de resultado general
// y redirecciona al detino especificado.
function FMensaje_Redirect(mensaje, url) {
    alert(mensaje);
    window.location = url;
}

function FMensaje(mensaje) {
    //alert(mensaje);
    setTimeout("alert('" + mensaje + "');", 0);
}

function FMensaje_Timeout(mensaje) {
    setTimeout("alert('" + mensaje + "');", 0);
}

function FCerrar(url) {
    var NumPages = history.length;
    if (NumPages > 0) {
        history.back();
    } else {
        window.location = url;
    }
}

function OnTreeClick(evt) {
    var src = window.event != window.undefined ? window.event.srcElement : evt.target;
    var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
    if (isChkBoxClick) {
        var parentTable = GetParentByTagName("table", src);
        var nxtSibling = parentTable.nextSibling;
        //check if nxt sibling is not null & is an element node
        if (nxtSibling && nxtSibling.nodeType == 1) {
            if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
            {
                //check or uncheck children at all levels
                CheckUncheckChildren(parentTable.nextSibling, src.checked);
            }
        }
        //check or uncheck parents at all levels
        CheckUncheckParents(src, src.checked);
    }
}

function GetParentByTagName(parentTagName, childElementObj) {
    var parent = childElementObj.parentNode;
    while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
        parent = parent.parentNode;
    }
    return parent;
}

function CheckUncheckChildren(childContainer, check) {
    var childChkBoxes = childContainer.getElementsByTagName("input");
    var childChkBoxCount = childChkBoxes.length;
    for (var i = 0; i < childChkBoxCount; i++) {
        childChkBoxes[i].checked = check;
    }
}

function CheckUncheckParents(srcChild, check) {
    var parentDiv = GetParentByTagName("div", srcChild);
    var parentNodeTable = parentDiv.previousSibling;
    if (parentNodeTable) {
        var checkUncheckSwitch;
        if (check) //checkbox checked
        {
            var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
            if (isAllSiblingsChecked)
                checkUncheckSwitch = true;
            else
                return; //do not need to check parent if any(one or more) child not checked
        }
        else //checkbox unchecked
        {
            checkUncheckSwitch = false;
        }

        var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
        if (inpElemsInParentTable.length > 0) {
            var parentNodeChkBox = inpElemsInParentTable[0];
            parentNodeChkBox.checked = checkUncheckSwitch;
            //do the same recursively
            CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
        }
    }
}

function AreAllSiblingsChecked(chkBox) {
    var parentDiv = GetParentByTagName("div", chkBox);
    var childCount = parentDiv.childNodes.length;
    for (var i = 0; i < childCount; i++) {
        if (parentDiv.childNodes[i].nodeType == 1) {
            //check if the child node is an element node
            if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                //if any of sibling nodes are not checked, return false
                if (!prevChkBox.checked) {
                    return false;
                }
            }
        }
    }
    return true;
}



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



