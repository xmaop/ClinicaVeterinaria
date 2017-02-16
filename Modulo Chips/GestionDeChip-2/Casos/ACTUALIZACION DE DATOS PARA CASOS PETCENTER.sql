/*---------------------------------------------------------------------------------------------------------------------
CUS 12
---------------------------------------------------------------------------------------------------------------------*/
/*
1. Caso restriccion de edad : implantaci�n de chip
Orden	:	42
*/
UPDATE dbo.GCP_OrdenAtencion SET estado = 20, observacion = '', descripcionMotivoRechazo = '', id_MotivoRechazo = null WHERE idOrdenAtencion = 20
DELETE FROM dbo.ACI_OrdenAtencionHistorial WHERE idOrdenAtencion = 20


/*
2. Caso Iniciar implantaci�n + Implantado 
Orden	:	21
*/
UPDATE dbo.GCP_OrdenAtencion SET estado = 20, observacion = '', id_MotivoRechazo = null WHERE idOrdenAtencion = 21
UPDATE dbo.ACI_Chip SET estado = 'Activo' WHERE id_chip = 2
DELETE FROM dbo.ACI_OrdenAtencionHistorial WHERE idOrdenAtencion = 21

/*
3. Caso Iniciar implantaci�n + Rechazado 
Orden	:	36
*/
UPDATE dbo.GCP_OrdenAtencion SET estado = 20, observacion = '', id_MotivoRechazo = null WHERE idOrdenAtencion = 36
UPDATE dbo.ACI_Chip SET estado = 'Activo' WHERE id_chip = 1
DELETE FROM dbo.ACI_OrdenAtencionHistorial WHERE idOrdenAtencion = 36

/*
4. Caso rechazo directo 
Orden	:	37
*/
UPDATE dbo.GCP_OrdenAtencion SET estado = 20, observacion = '', descripcionMotivoRechazo = '', id_MotivoRechazo = null WHERE idOrdenAtencion = 37
DELETE FROM dbo.ACI_OrdenAtencionHistorial WHERE idOrdenAtencion = 37


/*
5. Caso REGLA: Clientes tienen mas de un Paciente
Orden	:	36 y 37
			ACI-RN-05-Solicitud de implantaci�n
*/


/*---------------------------------------------------------------------------------------------------------------------
CUS 16
---------------------------------------------------------------------------------------------------------------------*/
--DELETE FROM GCP_OrdenAtencion WHERE idOrdenAtencion >= 30
/*
1. Caso Por Inserci�n de Chip 
Orden	:	Del cliente Oscar Mendoza Snyer
*/


/*
2.	Case Otros motivos : ejemplo Por cambio de due�o
Orden	:	27
*/
UPDATE dbo.GCP_OrdenAtencion SET estado = 31, observacion = '', descripcionMotivoRechazo = '', id_MotivoRechazo = null WHERE idOrdenAtencion = 27
UPDATE dbo.GCP_Paciente SET fechaFoto = GETDATE()-180 WHERE Id_Mascota = 6
DELETE FROM dbo.ACI_OrdenAtencionHistorial WHERE idOrdenAtencion = 27
UPDATE dbo.ACI_TarjetaIdentificacion SET estado = 'Activo' WHERE idTarjetaIdentificacion = 24


/*
3. Case Otros motivos : ejemplo Por p�rdidad de Tarjeta + Expiracion de Tarjeta 		
Orden	:	24
*/
UPDATE dbo.GCP_OrdenAtencion SET estado = 31, observacion = '', descripcionMotivoRechazo = '', id_MotivoRechazo = null WHERE idOrdenAtencion = 24
UPDATE dbo.GCP_Paciente SET fechaFoto = GETDATE()-180 WHERE Id_Mascota = 2
DELETE FROM dbo.ACI_OrdenAtencionHistorial WHERE idOrdenAtencion = 24
UPDATE dbo.ACI_TarjetaIdentificacion SET estado = 'Activo' WHERE idTarjetaIdentificacion = 1


/*
2. Caso generar tarjeta de identificaci�n
3. Caso rechazar una orden
Orden	:	29
*/
UPDATE dbo.GCP_OrdenAtencion SET estado = 31 WHERE idOrdenAtencion = 29
DELETE dbo.ACI_TarjetaIdentificacion WHERE id_Mascota = 9 


/*
4. Caso dar de baja por cualquiera de los 3 motivos
Orden	:	27
*/



SELECT * FROM dbo.GCP_OrdenAtencion  WHERE idOrdenAtencion = 37
SELECT * FROM dbo.ACI_Chip
SELECT * FROM dbo.ACI_TarjetaIdentificacion
TRUNCATE TABLE ACI_OrdenAtencionHistorial