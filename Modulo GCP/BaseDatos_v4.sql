USE [master]
GO
/****** Object:  Database [BDPetCenter4]    Script Date: 13/02/2017 8:25:44 ******/
CREATE DATABASE [BDPetCenter4]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BDPetCenter4', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BDPetCenter4.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BDPetCenter4_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BDPetCenter4_log.ldf' , SIZE = 15296KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BDPetCenter4] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BDPetCenter4].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BDPetCenter4] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BDPetCenter4] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BDPetCenter4] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BDPetCenter4] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BDPetCenter4] SET ARITHABORT OFF 
GO
ALTER DATABASE [BDPetCenter4] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BDPetCenter4] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BDPetCenter4] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BDPetCenter4] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BDPetCenter4] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BDPetCenter4] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BDPetCenter4] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BDPetCenter4] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BDPetCenter4] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BDPetCenter4] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BDPetCenter4] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BDPetCenter4] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BDPetCenter4] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BDPetCenter4] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BDPetCenter4] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BDPetCenter4] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BDPetCenter4] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BDPetCenter4] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BDPetCenter4] SET RECOVERY FULL 
GO
ALTER DATABASE [BDPetCenter4] SET  MULTI_USER 
GO
ALTER DATABASE [BDPetCenter4] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BDPetCenter4] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BDPetCenter4] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BDPetCenter4] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BDPetCenter4', N'ON'
GO
USE [BDPetCenter4]
GO
USE [BDPetCenter4]
GO
/****** Object:  Sequence [dbo].[seqFileNumber]    Script Date: 13/02/2017 8:25:44 ******/
CREATE SEQUENCE [dbo].[seqFileNumber] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 1
 MINVALUE 1
 MAXVALUE 99999
 CACHE  10 
GO
/****** Object:  StoredProcedure [dbo].[GCP_autenticarUsuario]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_autenticarUsuario]
@login varchar(50),
@password varchar(50),
@mensaje varchar(100) output
AS
DECLARE @existe int;
BEGIN
	set @existe = (select COUNT(1) from SEG_Usuario u where u.AL_USUA = @login and u.PW_USUA = @password );
	if (@existe = 0)
	begin
	set @mensaje = 'acceso denegado al sistema';
	end
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_delCliente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_delCliente]
@id_Cliente int
AS
BEGIN
	
	update GCP_Cliente
	set estado = 'I'
	where idCliente = @id_Cliente;
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_delPaciente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_delPaciente]
@id_Paciente int
AS
BEGIN
	
	update GCP_Paciente
	set estado = 'I'
	where Id_Mascota = @id_Paciente;
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getClienteById]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getClienteById]
@id_Cliente int
AS
BEGIN
	select  cli.idCliente id_Cliente,
			cli.codigo,
			cli.Nom_Cliente nomCliente,
			cli.ApePat_Cliente apePatCliente,
			cli.ApeMat_Cliente apeMatCliente,
			cli.Documento_Identidad nroDocumento,
			cli.Telefono telefonoFijo,
			cli.[Dirección] direccion,
			cli.email,
			cli.Tipo_Cliente tipoCliente,
			cli.TipoDocumento_Identidad tipoDocumento,
			cli.Razon_Social razonSocial,
			cli.Nombre_Contacto nomContacto,
			cli.Email_Contacto emailContacto,
			cli.celular,
			cli.fecha_Nacimiento fechaNacimiento,
			cli.sexo,
			cli.id_Distrito,
			dis.nombre descDistrito,
			tic.nombre descTipoCliente,
			doc1.codigo descTipoDocumento,
			doc2.codigo descTipoDocumentoContacto
	from GCP_Cliente cli
	left join GCP_Distrito dis on cli.id_Distrito = dis.id_Distrito
	left join GCP_TipoCliente tic on cli.Tipo_Cliente= tic.id_TipoCliente
	left join GCP_TipoDocumento doc1 on cli.TipoDocumento_Identidad = doc1.id_TipoDocumento
	left join GCP_TipoDocumento doc2 on cli.TipoDocIdent_Contacto = doc2.id_TipoDocumento
	where cli.idCliente = @id_Cliente;
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getClientesANotificar]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getClientesANotificar]
@id_Ordenes varchar(400)
AS
BEGIN
	select  oa.idOrdenAtencion id_OrdenAtencion,
			cli.idCliente id_Cliente,
			pa.Id_Mascota id_Paciente,
			oa.codigo,
			oa.fecha,
			tu.horaInicio,
			tu.horaFin,
			sd.nombre descSede,
			se.descripcion descServicio,
			case when cli.razon_social is null then cli.Nom_Cliente + ' ' + cli.ApePat_Cliente + ' ' + cli.ApePat_Cliente
			else cli.Razon_Social end nomCliente,
			cli.codigo codigoCliente,
			tcli.nombre descTipoCliente,
			cli.Email emailCliente,
			cli.celular celularCliente,
			--tdoc.codigo descTipoDocCliente,
			--cli.Documento_Identidad nroDocCliente,
			--pa.Nombre nomPaciente,
			--pa.codigo_Mascota codigoPaciente,
			--est.nombre descEstado,
			oa.estado
	from GCP_OrdenAtencion oa
	inner join GCP_Servicio se on oa.id_Servicio = se.id_Servicio
	inner join GCP_Sede sd on oa.id_Sede = sd.id_Sede
	inner join GCP_Paciente pa on oa.id_Mascota = pa.Id_Mascota
	inner join GCP_Cliente cli on pa.Id_Cliente = cli.idCliente
	inner join GCP_TipoDocumento tdoc on cli.TipoDocumento_Identidad = tdoc.id_TipoDocumento
	inner join GCP_TipoCliente tcli on cli.Tipo_Cliente = tcli.id_TipoCliente
	inner join GCP_EstadoOrden est on oa.estado = est.codigo
	inner join GCP_Turno tu on oa.id_Turno = tu.id_Turno
	where oa.idOrdenAtencion in (
		SELECT Value
		FROM SplitString(@id_Ordenes, ',')	 
	);
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_getDetalleNotificacion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getDetalleNotificacion]
@id_OrdenAtencion int
AS
BEGIN
	select notif.id_Notificacion,
		notif.asunto,
		notif.detalle,
		notif.fechaEnvio,
		case when cli.razon_social is null then cli.Nom_Cliente + ' ' + cli.ApePat_Cliente + ' ' + cli.ApePat_Cliente
			else cli.Razon_Social end nomCliente,
		case 
		when ot.flgNotificar = 'S' then 'Mensaje de texto (SMS)'
		when ot.flgNotificar = 'E' then 'Correo Electrónico (Email)' end descTipoEnvio
	from GCP_Notificacion notif
	inner join GCp_OrdenAtencion ot
	on notif.id_OrdenAtencion = ot.idOrdenAtencion
	inner join GCP_Paciente pac
	on ot.id_Mascota = pac.Id_Mascota
	inner join GCP_Cliente cli
	on cli.idCliente = pac.Id_Cliente
	where notif.id_OrdenAtencion = @id_OrdenAtencion;
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_getDistrito]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getDistrito]
AS
BEGIN
	select id_Distrito,
		   nombre
	from GCP_Distrito
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getEspeciePaciente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getEspeciePaciente]
AS
BEGIN
	select id_Especie,
		   descripcion
	from GCP_Especie
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getEstadoOrden]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getEstadoOrden]
AS
BEGIN
	select codigo,
		nombre descripcion
	from GCP_EstadoOrden
	order by codigo desc
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getGenero]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getGenero]
AS
BEGIN
	select 'M' codigo, 'Masculino' descripcion
	union
	select 'F' codigo, 'Femenino' descripcion
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getGeneroPaciente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getGeneroPaciente]
AS
BEGIN
	select 'M' codigo, 'Macho' descripcion
	union
	select 'H' codigo, 'Hembra' descripcion
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getListadoCliente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getListadoCliente]
AS
BEGIN
	select  cli.idCliente id_Cliente,
			cli.codigo,
			cli.Nom_Cliente nomCliente,
			cli.ApePat_Cliente apePatCliente,
			cli.ApeMat_Cliente apeMatCliente,
			cli.Documento_Identidad nroDocumento,
			cli.Telefono telefonoFijo,
			cli.[Dirección] direccion,
			cli.email,
			cli.Tipo_Cliente tipoCliente,
			cli.TipoDocumento_Identidad tipoDocumento,
			cli.Razon_Social razonSocial,
			cli.Nombre_Contacto nomContacto,
			cli.Email_Contacto emailContacto,
			cli.celular,
			cli.fecha_Nacimiento fechaNacimiento,
			cli.sexo,
			cli.id_Distrito,
			dis.nombre descDistrito,
			tic.nombre descTipoCliente,
			doc1.codigo descTipoDocumento,
			doc2.codigo descTipoDocumentoContacto
	from GCP_Cliente cli
	left join GCP_Distrito dis on cli.id_Distrito = dis.id_Distrito
	left join GCP_TipoCliente tic on cli.Tipo_Cliente= tic.id_TipoCliente
	left join GCP_TipoDocumento doc1 on cli.TipoDocumento_Identidad = doc1.id_TipoDocumento
	left join GCP_TipoDocumento doc2 on cli.TipoDocIdent_Contacto = doc2.id_TipoDocumento
	where cli.estado = 'A';
END


GO
/****** Object:  StoredProcedure [dbo].[GCP_getListadoClienteActivos]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getListadoClienteActivos]
AS
BEGIN
	select  cli.idCliente id_Cliente,
			cli.codigo,
			cli.Nom_Cliente nomCliente,
			cli.ApePat_Cliente apePatCliente,
			cli.ApeMat_Cliente apeMatCliente,
			cli.Documento_Identidad nroDocumento,
			cli.Tipo_Cliente tipoCliente,
			cli.TipoDocumento_Identidad tipoDocumento,
			cli.Razon_Social razonSocial,
			tic.nombre descTipoCliente,
			doc1.codigo descTipoDocumento
	from GCP_Cliente cli
	left join GCP_TipoCliente tic on cli.Tipo_Cliente= tic.id_TipoCliente
	left join GCP_TipoDocumento doc1 on cli.TipoDocumento_Identidad = doc1.id_TipoDocumento
	where cli.estado = 'A';
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getListadoClienteHistorico]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getListadoClienteHistorico]
@id_Paciente int
AS
BEGIN
	select  cli.idCliente id_Cliente,
			cli.codigo,
			cli.Nom_Cliente nomCliente,
			cli.ApePat_Cliente apePatCliente,
			cli.ApeMat_Cliente apeMatCliente,
			cli.Documento_Identidad nroDocumento,
			cli.Tipo_Cliente tipoCliente,
			cli.TipoDocumento_Identidad tipoDocumento,
			cli.Razon_Social razonSocial,
			tic.nombre descTipoCliente,
			doc1.codigo descTipoDocumento,
			hist.fechaAsignacion fechaRegistro,
			hist.fechaRegistro fechaCese
	from GCP_Cliente cli
	left join GCP_TipoCliente tic on cli.Tipo_Cliente = tic.id_TipoCliente
	left join GCP_TipoDocumento doc1 on cli.TipoDocumento_Identidad = doc1.id_TipoDocumento
	inner join GCP_ClientePacienteHistorico hist on cli.idCliente = hist.id_Cliente
	where hist.id_Paciente = @id_Paciente;
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getListadoOrdenAtencion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getListadoOrdenAtencion]
@fechaInicio varchar(10),
@fechaFin varchar(10),
@id_Servicio varchar(10),
@id_Sede varchar(10),
@estado varchar(2),
@nomCliente varchar(200),
@codigoCliente varchar(10),
@id_TipoDocumento varchar(10),
@nroDocCliente varchar(11),
@id_TipoCliente varchar(10),
@nomPaciente varchar(50),
@codigoPaciente varchar(12)
AS
BEGIN
	select  oa.idOrdenAtencion id_OrdenAtencion,
			cli.idCliente id_Cliente,
			pa.Id_Mascota id_Paciente,
			oa.codigo,
			oa.fecha,
			tu.horaInicio,
			tu.horaFin,
			sd.nombre descSede,
			se.descripcion descServicio,
			case when cli.razon_social is null then cli.Nom_Cliente + ' ' + cli.ApePat_Cliente + ' ' + cli.ApePat_Cliente
			else cli.Razon_Social end nomCliente,
			cli.codigo codigoCliente,
			tcli.nombre descTipoCliente,
			cli.Email emailCliente,
			cli.celular celularCliente,
			tdoc.codigo descTipoDocCliente,
			cli.Documento_Identidad nroDocCliente,
			pa.Nombre nomPaciente,
			pa.codigo_Mascota codigoPaciente,
			est.nombre descEstado,
			oa.estado
	from GCP_OrdenAtencion oa
	inner join GCP_Servicio se on oa.id_Servicio = se.id_Servicio
	--inner join GCP_SedeServicio sdet on se.id_Servicio = sdet.id_Servicio
	inner join GCP_Sede sd on oa.id_Sede = sd.id_Sede
	inner join GCP_Paciente pa on oa.id_Mascota = pa.Id_Mascota
	inner join GCP_Cliente cli on pa.Id_Cliente = cli.idCliente
	inner join GCP_TipoDocumento tdoc on cli.TipoDocumento_Identidad = tdoc.id_TipoDocumento
	inner join GCP_TipoCliente tcli on cli.Tipo_Cliente = tcli.id_TipoCliente
	inner join GCP_EstadoOrden est on oa.estado = est.codigo
	inner join GCP_Turno tu on oa.id_Turno = tu.id_Turno
	where 
	(@fechaInicio is null or @fechaInicio = '' or oa.fecha between cast(@fechaInicio as date) and case when @fechaFin is null or @fechaFin = '' then cast(getdate() as date) else cast(@fechaFin as date) end) and
	--(@fechaInicio is null or @fechaInicio = '' or CONVERT(VARCHAR(10), oa.fecha, 103) like '%' + @fechaInicio + '%') and
	(@id_Servicio is null or  @id_Servicio = '' or se.id_Servicio = cast(@id_Servicio as int)) and 
	(@id_Sede is null or @id_Sede = '' or sd.id_Sede = cast(@id_Sede as int)) and
	(@estado is null or @estado = '' or oa.estado = @estado) and
	(@nomCliente is null or @nomCliente = '' or
	@id_TipoCliente is null or @id_TipoCliente = '' or
	case when @id_TipoCliente = 1 then cli.Nom_Cliente + ' ' + cli.ApePat_Cliente + ' ' + cli.ApeMat_Cliente else cli.Razon_Social end like '%' + @nomCliente + '%') and
	 (@codigoCliente is null or @codigoCliente = '' or cli.codigo like '%'+ @codigoCliente + '%') and
	 (@id_TipoDocumento is null or @id_TipoDocumento = '' or cli.TipoDocumento_Identidad = cast(@id_TipoDocumento as int)) and
	 (@nroDocCliente is null or @nroDocCliente = '' or cli.Documento_Identidad like '%' + @nroDocCliente + '%') and
	 (@id_TipoCliente is null or @id_TipoCliente = '' or cli.Tipo_Cliente = cast(@id_TipoCliente as int)) and
	 (@nomPaciente is null or @nomPaciente = '' or pa.Nombre like '%' + @nomPaciente + '%') and
	 (@codigoPaciente is null or @codigoPaciente = '' or pa.codigo_Mascota like '%' + @codigoPaciente + '%')
	 
END


GO
/****** Object:  StoredProcedure [dbo].[GCP_getListadoOrdenAtencionNotif]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getListadoOrdenAtencionNotif]
@fechaInicio varchar(10),
@fechaFin varchar(10),
@id_Sede varchar(10),
@estado varchar(2),
@flgNotificar char(1)
AS
BEGIN
	select  oa.idOrdenAtencion id_OrdenAtencion,
			cli.idCliente id_Cliente,
			pa.Id_Mascota id_Paciente,
			oa.codigo,
			oa.fecha,
			tu.horaInicio,
			tu.horaFin,
			sd.nombre descSede,
			se.descripcion descServicio,
			case when cli.razon_social is null then cli.Nom_Cliente + ' ' + cli.ApePat_Cliente + ' ' + cli.ApePat_Cliente
			else cli.Razon_Social end nomCliente,
			cli.codigo codigoCliente,
			tcli.nombre descTipoCliente,
			cli.Email emailCliente,
			cli.celular celularCliente,
			tdoc.codigo descTipoDocCliente,
			cli.Documento_Identidad nroDocCliente,
			pa.Nombre nomPaciente,
			pa.codigo_Mascota codigoPaciente,
			est.nombre descEstado,
			oa.estado,
			oa.flgNotificar
	from GCP_OrdenAtencion oa
	inner join GCP_Servicio se on oa.id_Servicio = se.id_Servicio
	inner join GCP_Sede sd on oa.id_Sede = sd.id_Sede
	inner join GCP_Paciente pa on oa.id_Mascota = pa.Id_Mascota
	inner join GCP_Cliente cli on pa.Id_Cliente = cli.idCliente
	inner join GCP_TipoDocumento tdoc on cli.TipoDocumento_Identidad = tdoc.id_TipoDocumento
	inner join GCP_TipoCliente tcli on cli.Tipo_Cliente = tcli.id_TipoCliente
	inner join GCP_EstadoOrden est on oa.estado = est.codigo
	inner join GCP_Turno tu on oa.id_Turno = tu.id_Turno
	where 
	(@fechaInicio is null or @fechaInicio = '' or oa.fecha between cast(@fechaInicio as date) and case when @fechaFin is null or @fechaFin = '' then cast(getdate() as date) else cast(@fechaFin as date) end) and
	(@id_Sede is null or @id_Sede = '' or sd.id_Sede = cast(@id_Sede as int)) and
	--(@estado is null or @estado = '' or oa.estado = @estado) and
	(@flgNotificar is null or @flgNotificar = '' or oa.flgNotificar = @flgNotificar)
	 
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_getListadoPaciente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getListadoPaciente]
AS
BEGIN
	select  paci.Id_Cliente,
			paci.Id_Mascota id_Paciente,
			cli.codigo codigoCliente,
			case when cli.Tipo_Cliente = 1 then 
				cli.Nom_Cliente + ' ' + cli.ApePat_Cliente + ' ' + cli.ApeMat_Cliente
				else cli.Razon_Social end nomCliente,
			paci.nombre,
			paci.codigo_Mascota codigo,
			paci.Fecha_Nacimiento fechaNacimiento,
			paci.Sexo,
			case when paci.Sexo = 'M' then 'Macho' else 'Hembra' end descSexo,
			paci.id_Especie,
			paci.id_Raza,
			paci.peso,
			esp.descripcion nomEspecie,
			raz.descripcion nomRaza,
			paci.Foto rutaImagen,
			paci.id_Foto,
			paci.estado
	from GCP_Paciente paci
	inner join GCP_Especie esp on paci.id_Especie = esp.id_Especie
	inner join GCP_Raza raz on paci.id_Raza = raz.id_Raza
	inner join GCP_Cliente cli on paci.Id_Cliente = cli.idCliente
	where cli.estado = 'A' and paci.estado = 'A';
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_getListadoPacientesByCliente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getListadoPacientesByCliente]
@id_Cliente int
AS
BEGIN
	select  paci.Id_Mascota id_Paciente,
			paci.nombre,
			paci.codigo_Mascota codigo,
			paci.Fecha_Nacimiento fechaNacimiento,
			paci.Sexo,
			case when paci.Sexo = 'M' then 'Macho' else 'Hembra' end descSexo,
			paci.id_Especie,
			paci.id_Raza,
			esp.descripcion nomEspecie,
			raz.descripcion nomRaza,
			paci.Foto rutaImagen,
		        paci.id_Foto
	from GCP_Paciente paci
	inner join GCP_Especie esp on paci.id_Especie = esp.id_Especie
	inner join GCP_Raza raz on paci.id_Raza = raz.id_Raza
	where paci.Id_Cliente = @id_Cliente and paci.estado = 'A';
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getPacienteById]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getPacienteById]
@id_Paciente int
AS
BEGIN
	select  paci.id_Cliente,
			paci.Id_Mascota id_Paciente,
			paci.nombre,
			paci.Fecha_Nacimiento fechaNacimiento,
			paci.sexo,
			paci.peso,
			case when paci.Sexo = 'M' then 'Macho' else 'Hembra' end descSexo,
			paci.Foto rutaImagen,
			paci.id_Foto,
			paci.codigo_Mascota codigo,
			paci.id_chip,
			paci.id_Raza,
			paci.id_Especie,
			esp.descripcion nomEspecie,
			raz.descripcion nomRaza,
			paci.estado,
			paci.comentario,
			cli.codigo codigoCliente,
			case when cli.Tipo_Cliente = 1 then 
				cli.Nom_Cliente + ' ' + cli.ApePat_Cliente + ' ' + cli.ApeMat_Cliente
				else cli.Razon_Social end nomCliente
	from GCP_Paciente paci
	inner join GCP_Especie esp on paci.id_Especie = esp.id_Especie
	inner join GCP_Raza raz on paci.id_Raza = raz.id_Raza
	inner join GCP_Cliente cli on paci.Id_Cliente = cli.idCliente
	where paci.Id_Mascota = @id_Paciente;
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getParametroByTipo]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getParametroByTipo]
@codigo varchar(50)
AS
BEGIN
	select id_Parametro,
		   codigo,
		   descripcion,
		   orden
	from GCP_parametro
	where codigo = @codigo
	order by orden asc;
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_getRazaByEspeciePaciente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getRazaByEspeciePaciente]
@id_Especie int
AS
BEGIN
	select id_Raza,
		   descripcion
	from GCP_Raza
	where id_Especie = @id_Especie;
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getReporteAtencion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getReporteAtencion]
@fechaInicio varchar(10),
@fechaFin varchar(10)
AS
BEGIN
	select count(*) cantidad, oa.fechaRegistro from GCP_OrdenAtencion oa
	where oa.estado = 'AT'
	and (@fechaInicio is null or @fechaInicio = '' or oa.fechaRegistro between cast(@fechaInicio as date) and case when @fechaFin is null or @fechaFin = '' then cast(getdate() as date) else cast(@fechaFin as date) end)
	group by oa.fechaRegistro
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_getReporteEspecie]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getReporteEspecie]
@fechaInicio varchar(10),
@fechaFin varchar(10)
AS
BEGIN
	select count(*) cantidad, /*oa.fechaRegistro*/esp.descripcion descEspecie
	from /*GCP_OrdenAtencion oa
	inner join*/ GCP_Paciente pac
		--on oa.id_Mascota = pac.Id_Mascota
	inner join  GCP_Especie esp
		on pac.id_Especie = esp.id_Especie
	where (@fechaInicio is null or @fechaInicio = '' or cast(pac.fechaRegistro as date) between cast(@fechaInicio as date)
	and case when @fechaFin is null or @fechaFin = '' then cast(getdate() as date) else cast(@fechaFin as date) end)
	group by pac.id_Especie, esp.descripcion
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_getReporteIngreso]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getReporteIngreso]
@fechaInicio varchar(10),
@fechaFin varchar(10)
AS
BEGIN
	select count(*) cantidad, oa.fechaRegistro from GCP_OrdenAtencion oa
	where/* oa.estado = 'AT'
	and*/ (@fechaInicio is null or @fechaInicio = '' or oa.fechaRegistro between cast(@fechaInicio as date) and case when @fechaFin is null or @fechaFin = '' then cast(getdate() as date) else cast(@fechaFin as date) end)
	group by oa.fechaRegistro
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_getReporteServicioCliente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getReporteServicioCliente]
@fechaInicio varchar(10),
@fechaFin varchar(10),
@id_Cliente varchar(10)
AS
BEGIN
	select '1' idCliente,
	'1' id_Servicio,
	getDate() fechaAtencion,
	'Hospedaje' descServicio,
	'Kity' nomPaciente,
	0.00 monto
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_getSecuenciaFile]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getSecuenciaFile]
@seqFile varchar(10) output
AS
BEGIN
	set @seqFile = LEFT('00000' + CAST(NEXT VALUE FOR seqFileNumber as varchar(5)), 10);
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getSede]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getSede]
AS
BEGIN
	select id_Sede,
	       codigo,
	       nombre
	from GCP_Sede
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getServicio]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getServicio]
AS
BEGIN
	select id_Servicio,
	       codigo,
	       descripcion
	from GCP_Servicio
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getServicioBySede]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getServicioBySede]
@id_Sede int
AS
BEGIN
	select se.id_Servicio,
	       se.codigo,
	       se.descripcion
	from GCP_Servicio se
	inner join GCP_SedeServicio det
		on se.id_Servicio = det.id_Servicio
	where det.id_Sede = @id_Sede
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getTipoCliente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getTipoCliente]
AS
BEGIN
	select id_TipoCliente,
		   codigo,
		   nombre
	from GCP_TipoCliente
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getTipoDocumento]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getTipoDocumento]
AS
BEGIN
	select id_TipoDocumento,
		   codigo,
		   nombre
	from GCP_TipoDocumento
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_getTipoNotificar]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getTipoNotificar]
AS
BEGIN
	select 'E' codigo, 'Email' descripcion
	union
	select 'S' codigo, 'Sms' descripcion
	union
	select 'N' codigo, 'No' descripcion
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_getUsuarioByLogin]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_getUsuarioByLogin]
@login varchar(50)
AS
BEGIN
	select e.CO_USUA idUsuario,
		   e.NO_USUA Nombres,
		   e.AP_USUA apPaterno,
		   e.AM_USUA apMaterno,
		   e.DNI_USUA nroDocumento,
		   e.NO_EMAIL email,
		   r.DE_ROL cargo
	from SEG_Usuario e
	inner join SEG_UsuarioxRol ur
	on e.CO_USUA = ur.CO_USUA
	inner join SEG_Rol r
	on ur.CO_ROL = r.CO_ROL
	where e.AL_USUA = @login;
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_insCliente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_insCliente]
@nomCliente varchar(150),
@apePatCliente varchar(150),
@apeMatCliente varchar(150),
@nroDocumento varchar(11),
@telefonoFijo varchar(10),
@direccion varchar(250),
@email varchar(50),
@tipoCliente int,
@tipoDocumento int,
@razonSocial varchar(200),
@nomContacto varchar(30),
@emailContacto varchar(50),
@celular varchar(9),
@fechaNacimiento dateTime,
@sexo char(1),
@id_Distrito int,
@id_Cliente int output
AS
DECLARE @codTemp varchar(8);
DECLARE @codigo varchar(8);
BEGIN
	-- obtiene el id del ultimo cliente registrado + 1
	set @id_Cliente = (select isNull(max(idCliente), 0) + 1 from GCP_Cliente);
	-- Genera el codigo en base al tipo de cliente
	set @codTemp = (select isnull(max(codigo), case when @tipoCliente = 1 then 'CNVP0000' else 'CJVP0000' end ) from GCP_Cliente where Tipo_Cliente = @tipoCliente);
	set @codigo = (SELECT STUFF(@codTemp, PATINDEX('%[0-9^]%', @codTemp), 4, '') + 
   RIGHT ('0000' + cast(cast(STUFF(@codTemp, PATINDEX('%[^0-9]%', @codTemp), 4, '') as int) + 1 as varchar), 4));

	insert into GCP_Cliente(idCliente,
							Nom_Cliente,
							ApePat_Cliente,
							ApeMat_Cliente,
							Documento_Identidad,
							Telefono,
							[Dirección],
							Email,
							Tipo_Cliente,
							TipoDocumento_Identidad,
							Razon_Social,
							Nombre_Contacto,
							Email_Contacto,
							celular,
							fecha_Nacimiento,
							sexo,
							id_Distrito,
							codigo,
							fechaRegistro,
							estado)
	values (
				@id_Cliente,
				@nomCliente,
				@apePatCliente,
				@apeMatCliente,
				@nroDocumento,
				@telefonoFijo,
				@direccion,
				@email,
				@tipoCliente,
				@tipoDocumento,
				@razonSocial,
				@nomContacto,
				@emailContacto,
				@celular,
				@fechaNacimiento,
				@sexo,
				@id_Distrito,
				@codigo,
				getDate(),
				'A'
	);
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_insPaciente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_insPaciente]
@nomPaciente varchar(50),
@fechaNacimiento datetime,
@sexo char(1),
@rutaImagen varchar(200),
@id_Foto varchar(10),
@peso numeric(5,2),
@id_Cliente int,
@id_Raza int,
@id_Especie int,
@comentario varchar(300),
@id_Paciente int output
AS
DECLARE @codTemp varchar(8);
DECLARE @codigo varchar(8);
BEGIN
select * from gcp_paciente
	-- obtiene el id del ultimo paciente registrado + 1
	set @id_Paciente = (select isNull(max(Id_Mascota), 0) + 1 from GCP_Paciente);

	-- Genera el codigo en base al tipo de cliente
	set @codTemp = (select isnull(max(codigo_Mascota), 'MAS' + cast( year( getdate()) as varchar) ) from GCP_Paciente);
	set @codigo = (SELECT STUFF(@codTemp, 8, 4, '') + 
   RIGHT ('0000' + cast(cast(STUFF(@codTemp, PATINDEX('%[^0-9]%', @codTemp), 7, '') as int) + 1 as varchar), 4));
  
	insert into GCP_Paciente(--id_Mascota,
							Nombre,
							Fecha_Nacimiento,
							Sexo,
							Foto,
							id_Foto,
							Peso,
							Id_Cliente,
							codigo_Mascota,
							id_Raza,
							id_Especie,
							comentario,
							fechaRegistro,
							estado)
	values (
				--cast(@id_Paciente as numeric(10,0)),
				@nomPaciente,
				@fechaNacimiento,
				@sexo,
				@rutaImagen,
				@id_Foto,
				@peso,
				@id_Cliente,
				@codigo,
				@id_Raza,
				@id_Especie,
				@comentario,
				getDate(),
				'A'
	);
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_updCliente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_updCliente]
@id_Cliente int,
@nomCliente varchar(150),
@apePatCliente varchar(150),
@apeMatCliente varchar(150),
@nroDocumento varchar(11),
@telefonoFijo varchar(10),
@direccion varchar(250),
@email varchar(50),
@tipoCliente int,
@tipoDocumento int,
@razonSocial varchar(200),
@nomContacto varchar(30),
@emailContacto varchar(50),
@celular varchar(9),
@fechaNacimiento dateTime,
@sexo char(1),
@id_Distrito int
AS
BEGIN
	
	update GCP_Cliente
	set Nom_Cliente = @nomCliente,
		ApePat_Cliente = @apePatCliente,
		ApeMat_Cliente = @apeMatCliente,
		Documento_Identidad = @nroDocumento,
		Telefono = @telefonoFijo,
		[Dirección] = @direccion,
		Email = @email,
		Tipo_Cliente = @tipoCliente,
		TipoDocumento_Identidad = @tipoDocumento,
		Razon_Social = @razonSocial,
		Nombre_Contacto = @nomContacto,
		Email_Contacto = @emailContacto,
		celular = @celular,
		fecha_Nacimiento = @fechaNacimiento,
		sexo = @sexo,
		id_Distrito = @id_Distrito
	where idCliente = @id_Cliente;
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_updClienteNotificado]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_updClienteNotificado]
@id_OrdenAtencion int,
@tipoEnvio char(1),
@asunto	varchar(300),
@detalle varchar(1500)
AS
BEGIN
	update GCP_OrdenAtencion
	set flgNotificar = @tipoEnvio
	where idOrdenAtencion = @id_OrdenAtencion;

	insert into GCP_Notificacion
	values(@asunto, @detalle, @id_OrdenAtencion, getDate());
END
GO
/****** Object:  StoredProcedure [dbo].[GCP_updEstadoOrdenAtencion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_updEstadoOrdenAtencion]
@id_OrdenAtencion int,
@estado char(2)
AS
BEGIN
	update GCP_OrdenAtencion
	set estado = @estado
	where idOrdenAtencion = @id_OrdenAtencion;
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_updPaciente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_updPaciente]
@id_Paciente int,
@nomPaciente varchar(50),
@fechaNacimiento datetime,
@sexo char(1),
@rutaImagen varchar(200),
@id_Foto varchar(10),
@peso numeric(5,2),
@id_Cliente int,
@id_Raza int,
@id_Especie int,
@comentario varchar(300)
AS
declare @vId_Cliente int;
declare @vfechaRegistro dateTime;
BEGIN
	-- Obtiene el Dueño Actual
	select @vId_Cliente = id_Cliente, @vfechaRegistro = fechaRegistro from GCP_Paciente where Id_Mascota = @id_Paciente;

	-- Si el cliente es distinto, entonces se crea un histórico
	if (@vId_Cliente != @id_Cliente)
	begin
		insert into GCP_ClientePacienteHistorico
			(id_Cliente, id_Paciente, fechaRegistro, fechaAsignacion)
		values
			(@vId_Cliente, @id_Paciente, getDate(), @vfechaRegistro);
	end;

	update GCP_Paciente
	set Nombre = @nomPaciente,
		Fecha_Nacimiento = @fechaNacimiento,
		Foto = @rutaImagen,
		id_Foto = @id_Foto,
		Peso = @peso,
		id_Cliente = @id_Cliente,
		comentario = @comentario,
		sexo = @sexo
	where id_Mascota = @id_Paciente;
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_valPacienteAsociado]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_valPacienteAsociado]
@id_Cliente int,
@mensaje varchar(100) output

AS
declare @cantidad int
BEGIN
	set @cantidad = (select count(*) from GCP_Paciente where Id_Cliente = @id_Cliente and estado = 'A');

	if (@cantidad > 0)
	set @mensaje = 'No se puede eliminar el cliente porque cuenta con uno o más pacientes asociados';
END

GO
/****** Object:  StoredProcedure [dbo].[GCP_valTipoDocCliente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GCP_valTipoDocCliente]
@id_Cliente int,
@nroDocumento varchar(12),
@id_TipoCliente int,
@mensaje varchar(100) output

AS
declare @idCliente int
BEGIN
	set @idCliente = (select isNull(idCliente,0) from gcp_cliente where Tipo_Cliente = @id_TipoCliente
	and [Documento_Identidad] = @nroDocumento);

	if (@idCliente != @id_Cliente)
	set @mensaje = 'Ya se encuentra registrado un cliente con el mismo Nro. Documento';
END

GO
/****** Object:  Table [dbo].[ACI_Chip]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACI_Chip](
	[id_chip] [numeric](10, 0) NOT NULL,
	[codigo_Chip] [nchar](10) NULL,
	[id_paciente] [numeric](10, 0) NOT NULL,
	[anoFabricacion] [nchar](10) NOT NULL,
	[marca] [nchar](100) NOT NULL,
	[estado] [nchar](20) NOT NULL,
	[fechaVencimiento] [date] NOT NULL,
	[fabricante] [nchar](100) NOT NULL,
 CONSTRAINT [PK_ACI_Chip] PRIMARY KEY CLUSTERED 
(
	[id_chip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ACI_HojaEscaneo]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ACI_HojaEscaneo](
	[idHojaEscaneo] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[fechaEscaneo] [date] NOT NULL,
	[Observacion] [varchar](500) NOT NULL,
	[Id_Empleado] [int] NOT NULL,
	[idOrdenAtencion] [int] NOT NULL,
 CONSTRAINT [PK_HojaEscaneo] PRIMARY KEY CLUSTERED 
(
	[idHojaEscaneo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ACI_MotivoRechazo]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ACI_MotivoRechazo](
	[id_MotivoRechazo] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](8) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_id_MotivoRechazo] PRIMARY KEY CLUSTERED 
(
	[id_MotivoRechazo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ACI_SolicitudAtencion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ACI_SolicitudAtencion](
	[idSolicitudAtencion] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[estado] [varchar](10) NOT NULL,
	[fechaRegistro] [date] NOT NULL,
	[tipoSolicitud] [varchar](20) NOT NULL,
	[id_Mascota] [numeric](10, 0) NOT NULL,
 CONSTRAINT [PK_idSolicitudAtencion] PRIMARY KEY CLUSTERED 
(
	[idSolicitudAtencion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ACI_TarjetaIdentificacion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ACI_TarjetaIdentificacion](
	[idTarjetaIdentificacion] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[fechaEmision] [varchar](10) NOT NULL,
	[estado] [varchar](200) NOT NULL,
	[fechaActualizacion] [date] NOT NULL,
	[id_Mascota] [numeric](10, 0) NOT NULL,
 CONSTRAINT [PK_idTarjetaIdentificacion] PRIMARY KEY CLUSTERED 
(
	[idTarjetaIdentificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_Cliente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_Cliente](
	[idCliente] [int] NOT NULL,
	[Nom_Cliente] [nvarchar](150) NULL,
	[ApePat_Cliente] [nvarchar](150) NULL,
	[ApeMat_Cliente] [nvarchar](150) NULL,
	[Documento_Identidad] [varchar](11) NULL,
	[Telefono] [nvarchar](10) NULL,
	[Dirección] [nvarchar](250) NULL,
	[Email] [nvarchar](150) NULL,
	[Tipo_Cliente] [int] NOT NULL,
	[TipoDocumento_Identidad] [int] NOT NULL,
	[Razon_Social] [varchar](200) NULL,
	[Nombre_Contacto] [varchar](30) NULL,
	[ApePat_Contacto] [nchar](10) NULL,
	[ApeMat_Contacto] [nchar](10) NULL,
	[TipoDocIdent_Contacto] [int] NULL,
	[NroDocIdent_Contacto] [nchar](10) NULL,
	[Email_Contacto] [varchar](150) NULL,
	[celular] [varchar](10) NULL,
	[fecha_Nacimiento] [datetime] NULL,
	[sexo] [char](1) NULL,
	[id_Distrito] [int] NULL,
	[codigo] [varchar](8) NULL,
	[estado] [char](1) NULL,
	[fechaRegistro] [datetime] NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_ClientePacienteHistorico]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GCP_ClientePacienteHistorico](
	[id_ClientePacienteHistorico] [int] IDENTITY(1,1) NOT NULL,
	[id_Cliente] [int] NOT NULL,
	[id_Paciente] [numeric](10, 0) NOT NULL,
	[fechaRegistro] [date] NOT NULL,
	[fechaAsignacion] [datetime] NOT NULL,
 CONSTRAINT [PK_id_ClientePacienteHistorico] PRIMARY KEY CLUSTERED 
(
	[id_ClientePacienteHistorico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GCP_Distrito]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_Distrito](
	[id_Distrito] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_id_Distrito] PRIMARY KEY CLUSTERED 
(
	[id_Distrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_Especie]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_Especie](
	[id_Especie] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
 CONSTRAINT [id_Especie] PRIMARY KEY CLUSTERED 
(
	[id_Especie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_EstadoOrden]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_EstadoOrden](
	[codigo] [varchar](2) NOT NULL,
	[nombre] [varchar](20) NOT NULL,
 CONSTRAINT [PK_id_EstadoOrden] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_Notificacion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_Notificacion](
	[id_Notificacion] [int] IDENTITY(1,1) NOT NULL,
	[asunto] [varchar](300) NULL,
	[detalle] [varchar](1500) NOT NULL,
	[id_OrdenAtencion] [int] NOT NULL,
	[fechaEnvio] [datetime] NOT NULL,
 CONSTRAINT [PK_Notificacion] PRIMARY KEY CLUSTERED 
(
	[id_Notificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_OrdenAtencion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_OrdenAtencion](
	[idOrdenAtencion] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](10) NOT NULL,
	[descripcion] [varchar](200) NOT NULL,
	[fecha] [date] NOT NULL,
	[observacion] [varchar](500) NULL,
	[descripciónMotivoRechazo] [nchar](10) NULL,
	[id_Mascota] [numeric](10, 0) NULL,
	[estado] [varchar](2) NOT NULL,
	[motivoGenerar] [varchar](80) NULL,
	[id_MotivoRechazo] [int] NULL,
	[id_Turno] [int] NOT NULL,
	[id_Servicio] [int] NOT NULL,
	[id_Sede] [int] NOT NULL,
	[flgNotificar] [char](1) NULL,
	[horaTurno] [varchar](10) NULL,
	[fechaRegistro] [datetime] NULL,
 CONSTRAINT [PK_idOrdenAtencion] PRIMARY KEY CLUSTERED 
(
	[idOrdenAtencion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_Paciente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_Paciente](
	[Id_Mascota] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Fecha_Nacimiento] [datetime] NULL,
	[Sexo] [nchar](1) NOT NULL,
	[Foto] [nvarchar](200) NULL,
	[Peso] [numeric](5, 2) NULL,
	[Id_Cliente] [int] NULL,
	[codigo_Mascota] [varchar](200) NULL,
	[id_chip] [numeric](10, 0) NULL,
	[id_Raza] [int] NULL,
	[id_Especie] [int] NULL,
	[comentario] [varchar](300) NULL,
	[estado] [char](1) NULL,
	[fechaRegistro] [datetime] NULL,
	[id_Foto] [varchar](10) NULL,
 CONSTRAINT [PK_Mascota] PRIMARY KEY CLUSTERED 
(
	[Id_Mascota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_Parametro]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_Parametro](
	[id_Parametro] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](20) NOT NULL,
	[descripcion] [varchar](2000) NOT NULL,
	[orden] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_Raza]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_Raza](
	[id_Raza] [int] IDENTITY(1,1) NOT NULL,
	[id_Especie] [int] NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
 CONSTRAINT [PK_id_Raza] PRIMARY KEY CLUSTERED 
(
	[id_Raza] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_Sede]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_Sede](
	[id_Sede] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](8) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[direccion] [varchar](300) NOT NULL,
	[id_Distrito] [int] NOT NULL,
	[telefono] [varchar](200) NULL,
 CONSTRAINT [PK_id_Sede] PRIMARY KEY CLUSTERED 
(
	[id_Sede] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_SedeServicio]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GCP_SedeServicio](
	[id_SedeServicio] [int] IDENTITY(1,1) NOT NULL,
	[id_Sede] [int] NOT NULL,
	[id_Servicio] [int] NOT NULL,
 CONSTRAINT [PK_id_SedeServicio] PRIMARY KEY CLUSTERED 
(
	[id_SedeServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GCP_Servicio]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_Servicio](
	[id_Servicio] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](8) NOT NULL,
	[descripcion] [varchar](300) NOT NULL,
 CONSTRAINT [PK_id_Servicio] PRIMARY KEY CLUSTERED 
(
	[id_Servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_TipoCliente]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_TipoCliente](
	[id_TipoCliente] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](8) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_id_TipoCliente] PRIMARY KEY CLUSTERED 
(
	[id_TipoCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_TipoDocumento]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_TipoDocumento](
	[id_TipoDocumento] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](8) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_id_TipoDocumento] PRIMARY KEY CLUSTERED 
(
	[id_TipoDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GCP_Turno]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GCP_Turno](
	[id_Turno] [int] IDENTITY(1,1) NOT NULL,
	[horaInicio] [varchar](6) NOT NULL,
	[horaFin] [varchar](6) NULL,
	[fechaTurno] [date] NOT NULL,
 CONSTRAINT [PK_id_Turno] PRIMARY KEY CLUSTERED 
(
	[id_Turno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Alimento]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GHA_Alimento](
	[Id_Tipo_Alimento] [numeric](6, 0) IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Tipo_Alimento] PRIMARY KEY CLUSTERED 
(
	[Id_Tipo_Alimento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GHA_Asignacion_turnos]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Asignacion_turnos](
	[id_AsignacionTurno] [int] NOT NULL,
	[FechaInicio] [datetime] NULL,
	[Turno] [varchar](100) NULL,
	[Estado] [bit] NULL,
	[Observacion] [varchar](300) NULL,
	[Id_Servicio] [int] NULL,
	[Id_Empleado] [int] NULL,
 CONSTRAINT [PK_Asignacion_turnos] PRIMARY KEY CLUSTERED 
(
	[id_AsignacionTurno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Canil]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Canil](
	[Id_Canil] [int] NOT NULL,
	[Tamanio] [varchar](100) NULL,
	[TipoEspecie] [int] NULL,
	[Estado] [bit] NULL,
	[Descripcion] [varchar](200) NULL,
 CONSTRAINT [PK_Canil] PRIMARY KEY CLUSTERED 
(
	[Id_Canil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Empleado]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Empleado](
	[id_Empleado] [int] NOT NULL,
	[Nombres] [varchar](150) NULL,
	[ApePaterno] [varchar](150) NULL,
	[ApeMaterno] [varchar](150) NULL,
	[Situacion] [varchar](150) NULL,
	[Cargo] [varchar](150) NULL,
	[idArea] [int] NOT NULL,
 CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED 
(
	[id_Empleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Entrada_Hospedaje]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Entrada_Hospedaje](
	[id_EntradaHospedaje] [int] NULL,
	[Numero_Registro] [varchar](100) NULL,
	[Fecha_Entrada] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Estado]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GHA_Estado](
	[Id_Estado] [nchar](1) NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[Id_Estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GHA_Expediente_Hospedaje]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GHA_Expediente_Hospedaje](
	[id_ExpHosp] [int] IDENTITY(1,1) NOT NULL,
	[Codigo_Expediente] [nvarchar](20) NOT NULL,
	[Fecha_Hospedaje] [datetime] NOT NULL,
	[Fecha_Salida] [datetime] NOT NULL,
	[Observacion] [nvarchar](50) NULL,
	[Estado] [int] NULL,
	[Id_Mascota] [numeric](10, 0) NOT NULL,
 CONSTRAINT [PK_ExpHosp] PRIMARY KEY CLUSTERED 
(
	[id_ExpHosp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GHA_Objetivo_Plan]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GHA_Objetivo_Plan](
	[Id_Objetivo] [numeric](10, 0) IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Objetivo_Plan] PRIMARY KEY CLUSTERED 
(
	[Id_Objetivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GHA_Plan_Alimenticio_Cab]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GHA_Plan_Alimenticio_Cab](
	[Id_Plan] [int] IDENTITY(1,1) NOT NULL,
	[Id_Servicio] [int] NULL,
	[Id_Objetivo] [numeric](10, 0) NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[Fecha_Inicio] [datetime] NULL,
	[Fecha_Fin] [datetime] NULL,
	[Fecha_Creacion] [datetime] NULL,
	[Estado] [nchar](1) NULL,
 CONSTRAINT [PK_Plan_Alimenticio_Cab] PRIMARY KEY CLUSTERED 
(
	[Id_Plan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GHA_Plan_Alimenticio_Det]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GHA_Plan_Alimenticio_Det](
	[Id_Det] [int] IDENTITY(1,1) NOT NULL,
	[Id_Plan] [int] NOT NULL,
	[Id_Secuencia] [numeric](3, 0) NULL,
	[Nombre_Plan] [nvarchar](200) NULL,
	[Fecha_Aplicacion] [datetime] NULL,
	[Hora_Aplicacion] [nchar](20) NULL,
	[Id_Tipo_Alimento] [numeric](6, 0) NULL,
	[Porcion] [decimal](16, 2) NULL,
	[Observacion] [nvarchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Det] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GHA_Plan_Medicamentos]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Plan_Medicamentos](
	[Id_PlanMedicamento] [int] NOT NULL,
	[id_TipoMedicamento] [int] NULL,
	[Dosis] [varchar](100) NULL,
	[Fecha] [datetime] NULL,
	[Suministrado] [bit] NULL,
	[observaciones] [varchar](300) NULL,
	[Id_Servicio] [int] NULL,
 CONSTRAINT [PK_Plan_Medicamentos] PRIMARY KEY CLUSTERED 
(
	[Id_PlanMedicamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Plan_Rutina]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Plan_Rutina](
	[Id_PlanRutina] [int] NOT NULL,
	[Fecha] [datetime] NULL,
	[id_TipoRutina] [int] NULL,
	[Observacion] [varchar](100) NULL,
	[Estado] [bit] NULL,
	[Id_Servicio] [int] NULL,
 CONSTRAINT [PK_Plan_Rutina] PRIMARY KEY CLUSTERED 
(
	[Id_PlanRutina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Plan_Rutina_Cab]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GHA_Plan_Rutina_Cab](
	[Id_Plan] [int] IDENTITY(1,1) NOT NULL,
	[Id_Servicio] [int] NULL,
	[Nombre] [nvarchar](100) NULL,
	[Fecha_Inicio] [datetime] NULL,
	[Fecha_Fin] [datetime] NULL,
	[Fecha_Creacion] [datetime] NULL,
	[Estado] [nchar](1) NULL,
 CONSTRAINT [PK_Plan_Rutina_Cab] PRIMARY KEY CLUSTERED 
(
	[Id_Plan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GHA_Plan_Rutina_Det]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GHA_Plan_Rutina_Det](
	[Id_Det] [int] IDENTITY(1,1) NOT NULL,
	[Id_Plan] [int] NOT NULL,
	[Id_Secuencia] [numeric](3, 0) NULL,
	[Nombre_Plan] [nvarchar](200) NULL,
	[Fecha_Aplicacion] [datetime] NULL,
	[Hora_Aplicacion] [nchar](20) NULL,
	[Id_Tipo_Rutina] [numeric](6, 0) NULL,
	[Observacion] [nvarchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Det] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GHA_Plan_Suplementos]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Plan_Suplementos](
	[Id_PlanSuplemento] [int] NOT NULL,
	[descripcion] [varchar](250) NULL,
	[id_TipoSuplemento] [int] NULL,
	[Dosis] [varchar](100) NULL,
	[Fecha] [datetime] NULL,
	[Suministrado] [bit] NULL,
	[Observaciones] [varchar](300) NULL,
	[Id_Servicio] [int] NULL,
 CONSTRAINT [PK_Plan_Suplementos] PRIMARY KEY CLUSTERED 
(
	[Id_PlanSuplemento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Recurso]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Recurso](
	[idRecurso] [int] NOT NULL,
	[CodigoRecurso] [varchar](100) NULL,
	[Descripcion] [varchar](200) NULL,
 CONSTRAINT [PK_Recurso] PRIMARY KEY CLUSTERED 
(
	[idRecurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Reserva_Cita]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Reserva_Cita](
	[Id_Reserva] [int] NOT NULL,
	[Cod_Reserva] [varchar](100) NULL,
	[tipo_Servicio] [varchar](100) NULL,
	[Fec_Inicio] [date] NULL,
	[Fec_Fin] [date] NULL,
	[Observacion] [varchar](300) NULL,
	[Id_Mascota] [numeric](10, 0) NULL,
	[estado] [int] NULL,
 CONSTRAINT [PK_Reserva_Cita] PRIMARY KEY CLUSTERED 
(
	[Id_Reserva] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Revision_Medica]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Revision_Medica](
	[id_Revision] [int] IDENTITY(1,1) NOT NULL,
	[Fecha_Revision] [datetime] NULL,
	[Observacion] [varchar](300) NULL,
	[Recomendacion] [varchar](300) NULL,
	[Resultado] [bit] NULL,
	[Id_Servicio] [int] NULL,
 CONSTRAINT [PK_Revision_Medica] PRIMARY KEY CLUSTERED 
(
	[id_Revision] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_ServicioHospedaje]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_ServicioHospedaje](
	[Id_Servicio] [int] IDENTITY(1,1) NOT NULL,
	[Codigo_Expediente] [nvarchar](20) NOT NULL,
	[Fecha_Ingreso] [datetime] NOT NULL,
	[Fecha_Salida] [datetime] NULL,
	[Observacion] [varchar](250) NULL,
	[Estado] [char](1) NULL,
	[Id_Reserva] [int] NULL,
	[Id_Canil] [int] NULL,
	[AuditEstado] [bit] NULL,
 CONSTRAINT [PK_Expediente] PRIMARY KEY CLUSTERED 
(
	[Id_Servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Ticket_Traslado]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Ticket_Traslado](
	[Id_Ticket] [int] NOT NULL,
	[Fecha] [datetime] NULL,
	[id_Tipotraslado] [int] NULL,
	[Observacion] [varchar](300) NULL,
	[Id_Servicio] [int] NULL,
 CONSTRAINT [PK_Ticket_Traslado] PRIMARY KEY CLUSTERED 
(
	[Id_Ticket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Tipo_Medicamento]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Tipo_Medicamento](
	[id_TipoMedicamento] [int] NOT NULL,
	[Descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_Tipo_Medicamento] PRIMARY KEY CLUSTERED 
(
	[id_TipoMedicamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Tipo_Rutina]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Tipo_Rutina](
	[id_TipoRutina] [int] NOT NULL,
	[Descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_Tipo_Rutina] PRIMARY KEY CLUSTERED 
(
	[id_TipoRutina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Tipo_Suplemento]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Tipo_Suplemento](
	[id_TipoSuplemento] [int] NOT NULL,
	[Descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_Tipo_Suplemento] PRIMARY KEY CLUSTERED 
(
	[id_TipoSuplemento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GHA_Tipo_Traslado]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GHA_Tipo_Traslado](
	[id_Tipotraslado] [int] NOT NULL,
	[Descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_Tipo_Traslado] PRIMARY KEY CLUSTERED 
(
	[id_Tipotraslado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_Area]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_Area](
	[idArea] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](20) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GPC_Area] PRIMARY KEY CLUSTERED 
(
	[idArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_CriterioEvaluacion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_CriterioEvaluacion](
	[idCriterioEvaluacion] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[Puntaje] [int] NOT NULL,
	[idEvaluacionProveedor] [int] NULL,
 CONSTRAINT [PK_GPC_CriterioEvaluacion] PRIMARY KEY CLUSTERED 
(
	[idCriterioEvaluacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_EvaluacionProveedor]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_EvaluacionProveedor](
	[idEvaluacionProveedor] [int] IDENTITY(1,1) NOT NULL,
	[NumEvaluación] [varchar](10) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Periodo] [varchar](10) NOT NULL,
	[Observacion] [varchar](200) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[idProveedor] [int] NOT NULL,
 CONSTRAINT [PK_GPC_EvaluacionProveedor] PRIMARY KEY CLUSTERED 
(
	[idEvaluacionProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_IncidenciaProveedor]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_IncidenciaProveedor](
	[idIncidenciaProveedor] [int] IDENTITY(1,1) NOT NULL,
	[NumIncidenciaProveedor] [varchar](20) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Descripción] [varchar](200) NOT NULL,
	[Penalidad] [int] NOT NULL,
	[idOrdenCompra] [int] NOT NULL,
 CONSTRAINT [PK_GPC_IncidenciaProveedor] PRIMARY KEY CLUSTERED 
(
	[idIncidenciaProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_ItemOrdenCompra]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GPC_ItemOrdenCompra](
	[idItemOrdenCompra] [int] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[idRecursoProveedor] [int] NOT NULL,
	[idOrdenCompra] [int] NOT NULL,
 CONSTRAINT [PK_GPC_ItemOrdenCompra] PRIMARY KEY CLUSTERED 
(
	[idItemOrdenCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GPC_ItemPlanCompras]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_ItemPlanCompras](
	[idItemPlanCompras] [int] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Observacion] [varchar](200) NOT NULL,
	[idRecursoProveedor] [int] NOT NULL,
	[idPlanCompras] [int] NOT NULL,
 CONSTRAINT [PK_GPC_ItemPlanCompras] PRIMARY KEY CLUSTERED 
(
	[idItemPlanCompras] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_ItemSolicitudRecursos]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GPC_ItemSolicitudRecursos](
	[idItemSolicitudRecursos] [int] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[idPresentacionRecurso] [int] NOT NULL,
	[IdSolicitudRecursos] [int] NOT NULL,
 CONSTRAINT [PK_GPC_ItemSolicitudRecursos] PRIMARY KEY CLUSTERED 
(
	[idItemSolicitudRecursos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GPC_OrdenCompra]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_OrdenCompra](
	[idOrdenCompra] [int] IDENTITY(1,1) NOT NULL,
	[NumOrdenCompra] [varchar](20) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[idEmpleado] [int] NOT NULL,
	[idProveedor] [int] NOT NULL,
	[idSolicitudRecursos] [int] NULL,
	[idPlanCompras] [int] NULL,
 CONSTRAINT [PK_GPC_OrdenCompra] PRIMARY KEY CLUSTERED 
(
	[idOrdenCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_PlanCompras]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_PlanCompras](
	[idPlanCompras] [int] IDENTITY(1,1) NOT NULL,
	[FechaRegistro] [date] NOT NULL,
	[FechaEjecucion] [date] NOT NULL,
	[Periodo] [varchar](10) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[idEmpleado] [int] NOT NULL,
 CONSTRAINT [PK_GPC_PlanCompras] PRIMARY KEY CLUSTERED 
(
	[idPlanCompras] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_PresentacionRecurso]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_PresentacionRecurso](
	[idPresentacionRecurso] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](20) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Factor] [int] NOT NULL,
	[idRecurso] [int] NOT NULL,
 CONSTRAINT [PK_GPC_PresentacionRecurso] PRIMARY KEY CLUSTERED 
(
	[idPresentacionRecurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_Presupuesto]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_Presupuesto](
	[idPresupuesto] [int] IDENTITY(1,1) NOT NULL,
	[Periodo] [varchar](10) NOT NULL,
	[Monto] [decimal](8, 2) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[idArea] [int] NOT NULL,
 CONSTRAINT [PK_GPC_Presupuesto] PRIMARY KEY CLUSTERED 
(
	[idPresupuesto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_Proveedor]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_Proveedor](
	[idProveedor] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](20) NOT NULL,
	[TipoDocumento] [varchar](5) NOT NULL,
	[Documento] [varchar](12) NOT NULL,
	[RazonSocial] [varchar](500) NOT NULL,
	[Direccion] [varchar](500) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Contacto] [varchar](200) NOT NULL,
	[Tipo] [varchar](20) NOT NULL,
	[Puntaje] [int] NOT NULL,
	[ESTADO] [varchar](10) NULL,
 CONSTRAINT [PK_GPC_Proveedor] PRIMARY KEY CLUSTERED 
(
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_Recurso]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_Recurso](
	[idRecurso] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](20) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
 CONSTRAINT [PK_GPC_Recurso] PRIMARY KEY CLUSTERED 
(
	[idRecurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GPC_RecursoProveedor]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GPC_RecursoProveedor](
	[idRecursoProveedor] [int] IDENTITY(1,1) NOT NULL,
	[ValorUnitario] [decimal](8, 2) NOT NULL,
	[idProveedor] [int] NOT NULL,
	[idPresentacionRecurso] [int] NOT NULL,
 CONSTRAINT [PK_GPC_RecursoProveedor] PRIMARY KEY CLUSTERED 
(
	[idRecursoProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GPC_SolicitudRecursos]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GPC_SolicitudRecursos](
	[idSolicitudRecursos] [int] IDENTITY(1,1) NOT NULL,
	[NumSolicitudRecursos] [varchar](20) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Prioridad] [bit] NOT NULL,
	[Observacion] [varchar](100) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[idEmpleado] [int] NOT NULL,
	[idArea] [int] NOT NULL,
	[idPlanCompras] [int] NULL,
 CONSTRAINT [PK_GPC_SolicitudRecursos] PRIMARY KEY CLUSTERED 
(
	[idSolicitudRecursos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SEG_Opcion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SEG_Opcion](
	[CO_OPCION] [numeric](18, 0) NULL,
	[NO_OPCION] [varchar](100) NULL,
	[RT_OPCION] [varchar](500) NULL,
	[ST_REGISTRO] [numeric](1, 0) NULL,
	[CO_USUA_CREA] [varchar](50) NULL,
	[FE_USUA_CREA] [datetime] NULL,
	[CO_USUA_MODI] [varchar](50) NULL,
	[FE_USUA_MODI] [datetime] NULL,
	[CO_OPCION_PADRE] [numeric](18, 0) NULL,
	[CO_APLICACION] [numeric](18, 0) NOT NULL,
	[CO_NIVEL] [numeric](18, 0) NULL,
	[TI_OPEN] [numeric](18, 0) NULL,
	[IMG_OPCION] [varchar](500) NULL,
	[DES_OPCION] [varchar](1000) NULL,
	[TI_PAR_RUTA] [numeric](18, 0) NULL,
	[AB_OPCION] [varchar](50) NULL,
	[NU_ORDEN] [numeric](3, 0) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SEG_Rol]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SEG_Rol](
	[CO_ROL] [numeric](18, 0) NOT NULL,
	[DE_ROL] [varchar](50) NULL,
	[ST_REGISTRO] [numeric](1, 0) NULL,
	[CO_USUA_CREA] [varchar](50) NULL,
	[FE_USUA_CREA] [datetime] NULL,
	[CO_USUA_MODI] [varchar](50) NULL,
	[FE_USUA_MODI] [datetime] NULL,
	[CO_APLICACION] [numeric](18, 0) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SEG_RolxOpcion]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SEG_RolxOpcion](
	[CO_ROL] [numeric](18, 0) NOT NULL,
	[CO_OPCION] [numeric](18, 0) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SEG_Usuario]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SEG_Usuario](
	[CO_USUA] [varchar](50) NOT NULL,
	[AL_USUA] [varchar](50) NOT NULL,
	[NO_USUA] [varchar](100) NULL,
	[AP_USUA] [varchar](100) NULL,
	[AM_USUA] [varchar](100) NULL,
	[PW_USUA] [varchar](100) NULL,
	[DNI_USUA] [varchar](50) NULL,
	[NO_EMAIL] [varchar](100) NULL,
	[NU_TELEFONO] [varchar](15) NULL,
	[DE_DIRECCION] [varchar](600) NULL,
	[ST_REGISTRO] [numeric](1, 0) NULL,
	[CO_AREA] [numeric](4, 0) NULL,
	[CO_USUA_CREA] [varchar](50) NULL,
	[FE_USUA_CREA] [datetime] NULL,
	[CO_USUA_MODI] [varchar](50) NULL,
	[FE_USUA_MODI] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SEG_UsuarioxRol]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SEG_UsuarioxRol](
	[CO_USUA] [varchar](50) NOT NULL,
	[CO_ROL] [numeric](18, 0) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 13/02/2017 8:25:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitString]
    (
        @List NVARCHAR(MAX),
        @Delim VARCHAR(255)
    )
    RETURNS TABLE
    AS
        RETURN ( SELECT [Value] FROM 
          ( 
            SELECT 
              [Value] = LTRIM(RTRIM(SUBSTRING(@List, [Number],
              CHARINDEX(@Delim, @List + @Delim, [Number]) - [Number])))
            FROM (SELECT Number = ROW_NUMBER() OVER (ORDER BY name)
              FROM sys.all_objects) AS x
              WHERE Number <= LEN(@List)
              AND SUBSTRING(@Delim + @List, [Number], LEN(@Delim)) = @Delim
          ) AS y
        );
GO
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (1, N'Roberto', N'Paredes', N'Quintanilla', N'45795881', N'4545454545', N'roberto@gmail.com', N'morellana@gmail.com', 1, 1, NULL, N'robertito', NULL, NULL, NULL, NULL, N'roberto@gmail.com', N'989288742', CAST(0x0000A6F000000000 AS DateTime), N'M', 10, N'CNVP0001', N'A', NULL)
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (2, N'Raul', N'Velarde', N'Carrillo', N'54829458', NULL, N'Sin numero', N'morellana@gmail.com', 1, 1, NULL, N'Juanito', NULL, NULL, NULL, NULL, N'juanito@gmail.com', N'989288742', CAST(0x00007E6A00000000 AS DateTime), N'M', 18, N'CNVP0002', N'A', NULL)
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (3, N'Meliza', N'Suarez', N'Palacios', N'10458795', NULL, N'Sin numero', N'glujantel@gmail.com', 1, 1, NULL, N'Alberto', NULL, NULL, NULL, NULL, N'kiarajes7@gmail.com', N'111111111', CAST(0x00008A8800000000 AS DateTime), N'M', 10, N'CNVP0003', N'A', NULL)
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (4, N'Lisett', N'Falcon', N'Espinoza', N'43584967', NULL, N'dirección', N'glujantel@gmail.com', 1, 1, NULL, N'Julian', NULL, NULL, NULL, NULL, N'jpinto@gmail.com', N'950949138', CAST(0x0000893F00000000 AS DateTime), N'F', 6, N'CNVP0004', N'A', NULL)
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (5, N'Jullyzza Junneth', N'Pérez', N'Rojas', N'40209429', N'991131614', N'Av. Lima 3345 SMP', N'kiarajes7@gmail.com', 1, 1, NULL, N'Juanita', NULL, NULL, NULL, NULL, N'kiarajes7@gmail.com', N'969742012', CAST(0x0000A70D00000000 AS DateTime), N'F', 25, N'CNVP0005', N'A', NULL)
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (6, NULL, NULL, NULL, N'10424578411', N'2342342343', N'Jr Neptuno 234  SMP', N'glujantel@gmail.com', 2, 2, N'Corporación Lancaster', N'Carlos Perez', NULL, NULL, NULL, NULL, N'glujantel@gmail.com', N'950949138', NULL, NULL, 21, N'CJVP0001', N'A', NULL)
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (7, NULL, NULL, NULL, N'20100154874', N'5994874', N'Jr Apolo XI  174 Urb Sol de Oro Los Olivos', N'glujantel@gmail.com', 2, 2, N'corporacion 2', N'javier carmona', NULL, NULL, NULL, NULL, N'jcarmona@gmail.com', N'950949138', NULL, NULL, 28, N'CJVP0002', N'I', NULL)
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (8, NULL, NULL, NULL, N'34534534534', N'23423432', N'swqwqr', N'glujantel@gmail.com', 2, 2, N'324324', N'ewrewrewr', NULL, NULL, NULL, NULL, N'rwe@gmail.com', N'950949138', NULL, NULL, 20, N'CJVP0003', N'I', NULL)
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (9, NULL, NULL, NULL, N'23423423423', N'123123123', N'Jr Evitamiento 2345', N'glujantel@gmail.com', 2, 2, N'Corporaci&#243;n Miyasato', N'Jaime Acuña', NULL, NULL, NULL, NULL, N'jacuna@miyasato.com', N'950949138', NULL, NULL, 17, N'CJVP0004', N'A', NULL)
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (10, NULL, NULL, NULL, N'42354353453', N'435345435', N'ninguno', N'glujantel@gmail.com', 2, 2, N'Corporación Jacinto', N'Juan Manuel', NULL, NULL, NULL, NULL, N'morellana@gmail.com', N'950949138', NULL, NULL, 19, N'CJVP0005', N'A', NULL)
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (11, N'Antuaneth', N'Montes', N'Alva', N'44578451', NULL, N'Av Brasil 123 SN', N'amontes@gmail.com', 1, 1, NULL, N'Antuaneth', NULL, NULL, NULL, NULL, N'amontes@gmail.com', N'951648741', CAST(0x00007C5F00000000 AS DateTime), N'F', 17, N'CNVP0006', N'A', CAST(0x0000A718015C4DF0 AS DateTime))
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (12, N'GianCarlo', N'Montenegro', N'Rodriguez', N'20654124', NULL, N'Jr. Apolo XI - 157 Urb. Los chasquis', N'gmontenegro@gmail.com', 1, 1, NULL, N'GianCarlo', NULL, NULL, NULL, NULL, N'gmontenegro@gmail.com', N'954875212', CAST(0x0000722C00000000 AS DateTime), N'M', 7, N'CNVP0007', N'A', CAST(0x0000A718015DA7EA AS DateTime))
INSERT [dbo].[GCP_Cliente] ([idCliente], [Nom_Cliente], [ApePat_Cliente], [ApeMat_Cliente], [Documento_Identidad], [Telefono], [Dirección], [Email], [Tipo_Cliente], [TipoDocumento_Identidad], [Razon_Social], [Nombre_Contacto], [ApePat_Contacto], [ApeMat_Contacto], [TipoDocIdent_Contacto], [NroDocIdent_Contacto], [Email_Contacto], [celular], [fecha_Nacimiento], [sexo], [id_Distrito], [codigo], [estado], [fechaRegistro]) VALUES (13, N'Elizabeth', N'Peve', N'Peña', N'44587451', NULL, N'Sin direccion', N'kamikaze@gmail.com', 1, 1, NULL, N'Elizabeth', NULL, NULL, NULL, NULL, N'kamikaze@gmail.com', N'957844510', CAST(0x00007A6000000000 AS DateTime), N'F', 30, N'CNVP0008', N'A', CAST(0x0000A718015E0487 AS DateTime))
SET IDENTITY_INSERT [dbo].[GCP_ClientePacienteHistorico] ON 

INSERT [dbo].[GCP_ClientePacienteHistorico] ([id_ClientePacienteHistorico], [id_Cliente], [id_Paciente], [fechaRegistro], [fechaAsignacion]) VALUES (4, 10, CAST(1 AS Numeric(10, 0)), CAST(0x693C0B00 AS Date), CAST(0x0000A0CD00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[GCP_ClientePacienteHistorico] OFF
SET IDENTITY_INSERT [dbo].[GCP_Distrito] ON 

INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (1, N'Cercado de Lima')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (2, N'Ate')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (3, N'Barranci')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (4, N'Breña')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (5, N'Comas')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (6, N'Chorrillos')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (7, N'El Agustino')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (8, N'Jesús María')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (9, N'La Molina')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (10, N'La Victoria')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (11, N'Lince')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (12, N'Magdalena del Mar')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (13, N'Miraflores')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (14, N'Pueblo Libre')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (15, N'Puente Piedra')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (16, N'Rimac')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (17, N'San Isidro')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (18, N'Independencia')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (19, N'San Juan de Miraflores')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (20, N'San Luis')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (21, N'San Martin de Porres')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (22, N'San Migue')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (23, N'Santiago de Surco')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (24, N'Surquillo')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (25, N'Villa María del Triunfo')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (26, N'San Juan de Lurigancho')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (27, N'Santa Rosa')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (28, N'Los Olivos')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (29, N'San Borja')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (30, N'Villa El Savador')
INSERT [dbo].[GCP_Distrito] ([id_Distrito], [nombre]) VALUES (31, N'Santa Anita')
SET IDENTITY_INSERT [dbo].[GCP_Distrito] OFF
SET IDENTITY_INSERT [dbo].[GCP_Especie] ON 

INSERT [dbo].[GCP_Especie] ([id_Especie], [descripcion]) VALUES (1, N'Perro')
INSERT [dbo].[GCP_Especie] ([id_Especie], [descripcion]) VALUES (2, N'Gato')
INSERT [dbo].[GCP_Especie] ([id_Especie], [descripcion]) VALUES (3, N'Aves')
INSERT [dbo].[GCP_Especie] ([id_Especie], [descripcion]) VALUES (4, N'Peces')
INSERT [dbo].[GCP_Especie] ([id_Especie], [descripcion]) VALUES (5, N'Reptiles')
INSERT [dbo].[GCP_Especie] ([id_Especie], [descripcion]) VALUES (6, N'Roedor')
SET IDENTITY_INSERT [dbo].[GCP_Especie] OFF
INSERT [dbo].[GCP_EstadoOrden] ([codigo], [nombre]) VALUES (N'AN', N'Anulado')
INSERT [dbo].[GCP_EstadoOrden] ([codigo], [nombre]) VALUES (N'AT', N'Atendido')
INSERT [dbo].[GCP_EstadoOrden] ([codigo], [nombre]) VALUES (N'GE', N'Generado')
SET IDENTITY_INSERT [dbo].[GCP_Notificacion] ON 

INSERT [dbo].[GCP_Notificacion] ([id_Notificacion], [asunto], [detalle], [id_OrdenAtencion], [fechaEnvio]) VALUES (15, N'Orden de Atención OATV0027 pendiente para la fecha 11/02/2017', N'Estimado(a) Lisett Falcon Espinoza,<br /><br />Se le recuerda que tiene la Orden de Atención Nro. OATV0027 programada para el día 11/02/2017 a las 10:40.<br /><br />Por favor, tomar las consideraciones del caso.<br /><br />Atte,<br />Veterinaria PetCenter', 41, CAST(0x0000A71800A31340 AS DateTime))
INSERT [dbo].[GCP_Notificacion] ([id_Notificacion], [asunto], [detalle], [id_OrdenAtencion], [fechaEnvio]) VALUES (16, N'Orden de Atención OATV0028 pendiente para la fecha 11/02/2017', N'Estimado(a) Jullyzza Junneth Pérez Rojas,<br /><br />Se le recuerda que tiene la Orden de Atención Nro. OATV0028 programada para el día 11/02/2017 a las 11:20.<br /><br />Por favor, tomar las consideraciones del caso.<br /><br />Atte,<br />Veterinaria PetCenter', 42, CAST(0x0000A71800A31340 AS DateTime))
INSERT [dbo].[GCP_Notificacion] ([id_Notificacion], [asunto], [detalle], [id_OrdenAtencion], [fechaEnvio]) VALUES (17, N'Orden de Atención OATV0029 pendiente para la fecha 11/02/2017', N'Estimado(a) Jullyzza Junneth Pérez Rojas,<br /><br />Se le recuerda que tiene la Orden de Atención Nro. OATV0029 programada para el día 11/02/2017 a las 13:00.<br /><br />Por favor, tomar las consideraciones del caso.<br /><br />Atte,<br />Veterinaria PetCenter', 43, CAST(0x0000A71800A31340 AS DateTime))
INSERT [dbo].[GCP_Notificacion] ([id_Notificacion], [asunto], [detalle], [id_OrdenAtencion], [fechaEnvio]) VALUES (18, N'Orden de Atención OATV0030 pendiente para la fecha 11/02/2017', N'Estimado(a) Raul Velarde Carrillo,<br /><br />Se le recuerda que tiene la Orden de Atención Nro. OATV0030 programada para el día 11/02/2017 a las 15:00.<br /><br />Por favor, tomar las consideraciones del caso.<br /><br />Atte,<br />Veterinaria PetCenter', 44, CAST(0x0000A71800A31340 AS DateTime))
INSERT [dbo].[GCP_Notificacion] ([id_Notificacion], [asunto], [detalle], [id_OrdenAtencion], [fechaEnvio]) VALUES (19, N'Orden de Atención OATV0031 pendiente para la fecha 11/02/2017', N'Estimado(a) Roberto Paredes Quintanilla,<br /><br />Se le recuerda que tiene la Orden de Atención Nro. OATV0031 programada para el día 11/02/2017 a las 16:00.<br /><br />Por favor, tomar las consideraciones del caso.<br /><br />Atte,<br />Veterinaria PetCenter', 45, CAST(0x0000A71800A31340 AS DateTime))
INSERT [dbo].[GCP_Notificacion] ([id_Notificacion], [asunto], [detalle], [id_OrdenAtencion], [fechaEnvio]) VALUES (20, NULL, N'Estimado(a) Roberto Paredes Paredes. Veterinaria PetCenter le recuerda que tiene la atención nro. OATV0001 programada para el día 12/02/2017.', 13, CAST(0x0000A7180148075B AS DateTime))
INSERT [dbo].[GCP_Notificacion] ([id_Notificacion], [asunto], [detalle], [id_OrdenAtencion], [fechaEnvio]) VALUES (21, N'Orden de Atención OATV0002 pendiente para la fecha 12/02/2017', N'Estimado(a) Roberto Paredes Paredes,<br /><br />Se le recuerda que tiene la Orden de Atención Nro. OATV0002 programada para el día 12/02/2017 a las 00:00.<br /><br />Por favor, tomar las consideraciones del caso.<br /><br />Atte,<br />Veterinaria PetCenter', 14, CAST(0x0000A71801485751 AS DateTime))
SET IDENTITY_INSERT [dbo].[GCP_Notificacion] OFF
SET IDENTITY_INSERT [dbo].[GCP_OrdenAtencion] ON 

INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (13, N'OATV0001', N'Orden de Atención - Chorrillos', CAST(0x733C0B00 AS Date), N'Traer al perro con sus medicamentos', NULL, CAST(1 AS Numeric(10, 0)), N'GE', NULL, NULL, 49, 4, 12, N'S', NULL, CAST(0x0000A71800000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (14, N'OATV0002', N'Orden de Atención - Miraflores', CAST(0x733C0B00 AS Date), N'', NULL, CAST(1 AS Numeric(10, 0)), N'GE', NULL, NULL, 50, 1, 15, N'E', NULL, CAST(0x0000A71800000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (15, N'OATV0003', N'Orden de Atención - Los Olivos', CAST(0x733C0B00 AS Date), N'', NULL, CAST(1 AS Numeric(10, 0)), N'GE', NULL, NULL, 51, 2, 16, N'N', NULL, CAST(0x0000A71800000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (16, N'OATV0004', N'Orden de Atención - Los Olivos', CAST(0x733C0B00 AS Date), N'', NULL, CAST(3 AS Numeric(10, 0)), N'GE', NULL, NULL, 52, 4, 16, N'N', NULL, CAST(0x0000A71800000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (19, N'OATV0005', N'Orden de Atención - Miraflores', CAST(0x733C0B00 AS Date), N'', NULL, CAST(6 AS Numeric(10, 0)), N'GE', NULL, NULL, 53, 5, 15, N'N', NULL, CAST(0x0000A71800000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (20, N'OATV0006', N'Orden de Atención - La Molina', CAST(0x733C0B00 AS Date), N'', NULL, CAST(8 AS Numeric(10, 0)), N'GE', NULL, NULL, 54, 3, 13, N'N', NULL, CAST(0x0000A71800000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (21, N'OATV0007', N'Orden de Atención - Chorrillos', CAST(0x743C0B00 AS Date), N'', NULL, CAST(1 AS Numeric(10, 0)), N'GE', NULL, NULL, 1, 1, 12, N'N', NULL, CAST(0x0000A71900000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (22, N'OATV0008', N'Orden de Atención - Miraflores', CAST(0x743C0B00 AS Date), N'', NULL, CAST(2 AS Numeric(10, 0)), N'GE', NULL, NULL, 2, 2, 15, N'N', NULL, CAST(0x0000A71900000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (23, N'OATV0009', N'Orden de Atención - Miraflores', CAST(0x743C0B00 AS Date), N'', NULL, CAST(3 AS Numeric(10, 0)), N'GE', NULL, NULL, 1, 3, 15, N'N', NULL, CAST(0x0000A71900000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (24, N'OATV0010', N'Orden de Atención - Los Olivos', CAST(0x743C0B00 AS Date), N'', NULL, CAST(6 AS Numeric(10, 0)), N'GE', NULL, NULL, 1, 5, 16, N'N', NULL, CAST(0x0000A71900000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (25, N'OATV0011', N'Orden de Atención - Los Olivos', CAST(0x743C0B00 AS Date), N'', NULL, CAST(8 AS Numeric(10, 0)), N'GE', NULL, NULL, 2, 9, 16, N'N', NULL, CAST(0x0000A71900000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (26, N'OATV0012', N'Orden de Atención - La Molina', CAST(0x753C0B00 AS Date), N'', NULL, CAST(1 AS Numeric(10, 0)), N'GE', NULL, NULL, 4, 3, 13, N'N', NULL, CAST(0x0000A71A00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (27, N'OATV0013', N'Orden de Atención - Lince', CAST(0x753C0B00 AS Date), N'', NULL, CAST(1 AS Numeric(10, 0)), N'GE', NULL, NULL, 5, 4, 14, N'N', NULL, CAST(0x0000A71A00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (28, N'OATV0014', N'Orden de Atención - Lince', CAST(0x753C0B00 AS Date), N'', NULL, CAST(1 AS Numeric(10, 0)), N'GE', NULL, NULL, 6, 5, 14, N'N', NULL, CAST(0x0000A71A00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (29, N'OATV0015', N'Orden de Atención - La Molina', CAST(0x753C0B00 AS Date), N'', NULL, CAST(2 AS Numeric(10, 0)), N'GE', NULL, NULL, 7, 6, 13, N'N', NULL, CAST(0x0000A71A00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (30, N'OATV0016', N'Orden de Atención - Chorrillos', CAST(0x753C0B00 AS Date), N'', NULL, CAST(3 AS Numeric(10, 0)), N'GE', NULL, NULL, 8, 9, 12, N'N', NULL, CAST(0x0000A71A00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (31, N'OATV0017', N'Orden de Atención - Chorrillos', CAST(0x763C0B00 AS Date), N'', NULL, CAST(2 AS Numeric(10, 0)), N'GE', NULL, NULL, 12, 9, 12, N'N', NULL, CAST(0x0000A71B00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (32, N'OATV0018', N'Orden de Atención - Miraflores', CAST(0x763C0B00 AS Date), N'', NULL, CAST(3 AS Numeric(10, 0)), N'GE', NULL, NULL, 13, 8, 15, N'N', NULL, CAST(0x0000A71B00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (33, N'OATV0019', N'Orden de Atención - Miraflores', CAST(0x763C0B00 AS Date), N'', NULL, CAST(6 AS Numeric(10, 0)), N'GE', NULL, NULL, 14, 10, 15, N'N', NULL, CAST(0x0000A71B00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (34, N'OATV0020', N'Orden de Atención - La Molina', CAST(0x763C0B00 AS Date), N'', NULL, CAST(6 AS Numeric(10, 0)), N'GE', NULL, NULL, 15, 7, 13, N'N', NULL, CAST(0x0000A71B00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (35, N'OATV0021', N'Orden de Atención - La Molina', CAST(0x763C0B00 AS Date), N'', NULL, CAST(8 AS Numeric(10, 0)), N'GE', NULL, NULL, 16, 10, 13, N'N', NULL, CAST(0x0000A71B00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (36, N'OATV0022', N'Orden de Atención - Miraflores', CAST(0x773C0B00 AS Date), N'', NULL, CAST(2 AS Numeric(10, 0)), N'GE', NULL, NULL, 31, 5, 15, N'N', NULL, CAST(0x0000A71C00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (37, N'OATV0023', N'Orden de Atención - Chorrillos', CAST(0x773C0B00 AS Date), N'', NULL, CAST(2 AS Numeric(10, 0)), N'GE', NULL, NULL, 32, 5, 12, N'N', NULL, CAST(0x0000A71C00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (38, N'OATV0024', N'Orden de Atención - Miraflores', CAST(0x773C0B00 AS Date), N'', NULL, CAST(3 AS Numeric(10, 0)), N'GE', NULL, NULL, 33, 7, 15, N'N', NULL, CAST(0x0000A71C00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (39, N'OATV0025', N'Orden de Atención - Chorrillos', CAST(0x773C0B00 AS Date), N'', NULL, CAST(3 AS Numeric(10, 0)), N'GE', NULL, NULL, 34, 9, 12, N'N', NULL, CAST(0x0000A71C00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (40, N'OATV0026', N'Orden de Atención - Miraflores', CAST(0x773C0B00 AS Date), N'', NULL, CAST(3 AS Numeric(10, 0)), N'GE', NULL, NULL, 35, 8, 15, N'N', NULL, CAST(0x0000A71C00000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (41, N'OATV0027', N'Orden de Atención - Lince', CAST(0x723C0B00 AS Date), N'', NULL, CAST(6 AS Numeric(10, 0)), N'AT', NULL, NULL, 22, 1, 14, N'E', NULL, CAST(0x0000A71700000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (42, N'OATV0028', N'Orden de Atención - Lince', CAST(0x723C0B00 AS Date), N'', NULL, CAST(8 AS Numeric(10, 0)), N'AT', NULL, NULL, 23, 2, 14, N'E', NULL, CAST(0x0000A71700000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (43, N'OATV0029', N'Orden de Atención - La Molina', CAST(0x723C0B00 AS Date), N'', NULL, CAST(8 AS Numeric(10, 0)), N'AT', NULL, NULL, 24, 4, 13, N'E', NULL, CAST(0x0000A71700000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (44, N'OATV0030', N'Orden de Atención - Lince', CAST(0x723C0B00 AS Date), N'', NULL, CAST(2 AS Numeric(10, 0)), N'AT', NULL, NULL, 25, 3, 14, N'E', NULL, CAST(0x0000A71700000000 AS DateTime))
INSERT [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion], [codigo], [descripcion], [fecha], [observacion], [descripciónMotivoRechazo], [id_Mascota], [estado], [motivoGenerar], [id_MotivoRechazo], [id_Turno], [id_Servicio], [id_Sede], [flgNotificar], [horaTurno], [fechaRegistro]) VALUES (45, N'OATV0031', N'Orden de Atención - Miraflores', CAST(0x723C0B00 AS Date), N'', NULL, CAST(1 AS Numeric(10, 0)), N'AT', NULL, NULL, 26, 3, 15, N'E', NULL, CAST(0x0000A71700000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[GCP_OrdenAtencion] OFF
SET IDENTITY_INSERT [dbo].[GCP_Paciente] ON 

INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(1 AS Numeric(10, 0)), N'Harry', CAST(0x0000A0CD00000000 AS DateTime), N'M', N'img003.png', CAST(12.00 AS Numeric(5, 2)), 1, N'MAS20161001', NULL, 38, 1, NULL, N'A', CAST(0x0000A6FB00000000 AS DateTime), NULL)
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(2 AS Numeric(10, 0)), N'Kimo', CAST(0x0000A1BE00000000 AS DateTime), N'M', N'Rufo.jpg', CAST(15.00 AS Numeric(5, 2)), 2, N'MAS20161002', NULL, 39, 1, NULL, N'A', CAST(0x0000A6F200000000 AS DateTime), NULL)
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(3 AS Numeric(10, 0)), N'Chery', CAST(0x0000A57800000000 AS DateTime), N'M', N'DiagramaDeBD.png', CAST(5.00 AS Numeric(5, 2)), 3, N'MAS20161003', NULL, 29, 1, N'comentarioooo', N'A', CAST(0x0000A6F700000000 AS DateTime), N'00001.png')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(6 AS Numeric(10, 0)), N'Blacky', CAST(0x0000A37800000000 AS DateTime), N'M', N'BLAKY.jpg', CAST(13.00 AS Numeric(5, 2)), 4, N'MAS20161004', NULL, 37, 1, NULL, N'A', CAST(0x0000A6FF00000000 AS DateTime), NULL)
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(8 AS Numeric(10, 0)), N'Peluchin', CAST(0x00009EE300000000 AS DateTime), N'M', N'peluchin.jpg', CAST(5.00 AS Numeric(5, 2)), 5, N'MAS20161005', NULL, 29, 1, NULL, N'A', CAST(0x0000A6FF00000000 AS DateTime), NULL)
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(11 AS Numeric(10, 0)), N'Pelusa', CAST(0x0000A3DA00000000 AS DateTime), N'H', N'gato-balines.jpg', CAST(1.45 AS Numeric(5, 2)), 11, N'MAS20160', NULL, 43, 2, NULL, N'A', CAST(0x0000A70D015E93B0 AS DateTime), N'000007.jpg')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(12 AS Numeric(10, 0)), N'Katano', CAST(0x0000A6B300000000 AS DateTime), N'M', N'katano.jpg', CAST(0.45 AS Numeric(5, 2)), 11, N'MAS20160', NULL, 3, 1, NULL, N'A', CAST(0x0000A70D015F1421 AS DateTime), N'000008.jpg')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(13 AS Numeric(10, 0)), N'Puchini', CAST(0x0000A62800000000 AS DateTime), N'M', N'puchini.jpg', CAST(2.46 AS Numeric(5, 2)), 12, N'MAS20160', NULL, 18, 1, NULL, N'A', CAST(0x0000A70D015F75E2 AS DateTime), N'000009.jpg')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(14 AS Numeric(10, 0)), N'Ashlee', CAST(0x0000A67400000000 AS DateTime), N'H', N'fido.jpg', CAST(2.47 AS Numeric(5, 2)), 12, N'MAS20160', NULL, 23, 1, NULL, N'A', CAST(0x0000A711015FE4D1 AS DateTime), N'0000010.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(15 AS Numeric(10, 0)), N'karla', CAST(0x0000A6A500000000 AS DateTime), N'H', N'karla.jpg', CAST(1.62 AS Numeric(5, 2)), 3, N'MAS20160', NULL, 61, 2, NULL, N'A', CAST(0x0000A71101603D32 AS DateTime), N'0000011.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(16 AS Numeric(10, 0)), N'Crazo', CAST(0x0000A6F200000000 AS DateTime), N'M', N'crazo.jpg', CAST(0.42 AS Numeric(5, 2)), 12, N'MAS20160', NULL, 75, 3, NULL, N'A', CAST(0x0000A7120160C364 AS DateTime), N'0000012.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(17 AS Numeric(10, 0)), N'julissa', CAST(0x0000A6BC00000000 AS DateTime), N'H', N'julissa.jpg', CAST(0.74 AS Numeric(5, 2)), 13, N'MAS20160', NULL, 68, 3, NULL, N'A', CAST(0x0000A7120161275F AS DateTime), N'0000013.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(18 AS Numeric(10, 0)), N'Katiuska', CAST(0x0000A63E00000000 AS DateTime), N'H', N'Katiuska.jpg', CAST(2.78 AS Numeric(5, 2)), 13, N'MAS20160', NULL, 62, 2, NULL, N'A', CAST(0x0000A71501617EE8 AS DateTime), N'0000014.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(19 AS Numeric(10, 0)), N'negro', CAST(0x0000A67B00000000 AS DateTime), N'M', N'negro.jpg', CAST(2.75 AS Numeric(5, 2)), 1, N'MAS20160', NULL, 56, 2, NULL, N'A', CAST(0x0000A7150161FD4E AS DateTime), N'0000015.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(20 AS Numeric(10, 0)), N'kratos', CAST(0x0000A5B000000000 AS DateTime), N'M', N'kratos.jpg', CAST(0.86 AS Numeric(5, 2)), 2, N'MAS20160', NULL, 99, 5, NULL, N'A', CAST(0x0000A71601629BC2 AS DateTime), N'0000016.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(21 AS Numeric(10, 0)), N'carlitos', CAST(0x0000A6BA00000000 AS DateTime), N'M', N'carlitos.jpg', CAST(0.80 AS Numeric(5, 2)), 4, N'MAS20160', NULL, 108, 6, NULL, N'A', CAST(0x0000A7160162F769 AS DateTime), N'0000017.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(22 AS Numeric(10, 0)), N'dormilon', CAST(0x0000A52B00000000 AS DateTime), N'M', N'dormilon.jpg', CAST(0.45 AS Numeric(5, 2)), 3, N'MAS20160', NULL, 98, 5, NULL, N'A', CAST(0x0000A7170163866F AS DateTime), N'0000018.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(23 AS Numeric(10, 0)), N'parlanchina', CAST(0x0000A69800000000 AS DateTime), N'H', N'parlanchina.jpg', CAST(0.85 AS Numeric(5, 2)), 13, N'MAS20160', NULL, 78, 3, NULL, N'A', CAST(0x0000A7170163DB22 AS DateTime), N'0000019.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(24 AS Numeric(10, 0)), N'manchas', CAST(0x0000A43500000000 AS DateTime), N'M', N'manchas.jpg', CAST(1.97 AS Numeric(5, 2)), 9, N'MAS20160', NULL, 57, 2, NULL, N'A', CAST(0x0000A71801642534 AS DateTime), N'0000020.jp')
INSERT [dbo].[GCP_Paciente] ([Id_Mascota], [Nombre], [Fecha_Nacimiento], [Sexo], [Foto], [Peso], [Id_Cliente], [codigo_Mascota], [id_chip], [id_Raza], [id_Especie], [comentario], [estado], [fechaRegistro], [id_Foto]) VALUES (CAST(25 AS Numeric(10, 0)), N'Rufo', CAST(0x0000A4FB00000000 AS DateTime), N'M', N'rufo.jpg', CAST(2.97 AS Numeric(5, 2)), 13, N'MAS20160', NULL, 10, 1, NULL, N'A', CAST(0x0000A7180164B212 AS DateTime), N'0000021.jp')
SET IDENTITY_INSERT [dbo].[GCP_Paciente] OFF
SET IDENTITY_INSERT [dbo].[GCP_Parametro] ON 

INSERT [dbo].[GCP_Parametro] ([id_Parametro], [codigo], [descripcion], [orden]) VALUES (1, N'EMAIL', N'Orden de Atención {0} pendiente para la fecha {1}', 1)
INSERT [dbo].[GCP_Parametro] ([id_Parametro], [codigo], [descripcion], [orden]) VALUES (2, N'EMAIL', N'Estimado(a) {0},<br /><br />Se le recuerda que tiene la Orden de Atención Nro. {1} programada para el día {2} a las {3}.<br /><br />Por favor, tomar las consideraciones del caso.<br /><br />Atte,<br />Veterinaria PetCenter', 2)
INSERT [dbo].[GCP_Parametro] ([id_Parametro], [codigo], [descripcion], [orden]) VALUES (3, N'SMS', N'Estimado(a) {0}. Veterinaria PetCenter le recuerda que tiene la atención nro. {1} programada para el día {2}.', 1)
SET IDENTITY_INSERT [dbo].[GCP_Parametro] OFF
SET IDENTITY_INSERT [dbo].[GCP_Raza] ON 

INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (1, 1, N'Afgano')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (2, 1, N'Akita')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (3, 1, N'American Bully')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (4, 1, N'American Foxhound')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (5, 1, N'American Pit Bull Terrier')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (6, 1, N'American Staffordshire')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (7, 1, N'Basset artesiano')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (8, 1, N'Basset Hound')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (9, 1, N'Beagle Harrier')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (10, 1, N'Beagle')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (11, 1, N'Bichón Frisé')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (12, 1, N'Bichón Maltés')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (13, 1, N'Billy')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (14, 1, N'Bloodhound')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (15, 1, N'Border Collie')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (16, 1, N'Border Terrier')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (17, 1, N'Boston Terrier')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (18, 1, N'Bóxer')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (19, 1, N'Boyero Australiano')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (20, 1, N'Boyero de Flandes')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (21, 1, N'Braco Aleman ')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (22, 1, N'Braco francés ')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (23, 1, N'Bull Terrier')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (24, 1, N'Bulldog')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (25, 1, N'Bulldog Americano')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (26, 1, N'Bulldog Frances')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (27, 1, N'Cairn Terrier')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (28, 1, N'Caniche (poodle)')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (29, 1, N'Chihuahua')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (30, 1, N'Chow Chow')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (31, 1, N'Cocker Spanie')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (32, 1, N'Collie')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (33, 1, N'Dálmata')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (34, 1, N'Doberman')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (35, 1, N'Dogo Argentino')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (36, 1, N'Fila Brasileiro')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (37, 1, N'Pastor Alemán')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (38, 1, N'Yorkshire terrier')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (39, 1, N'Pug')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (40, 2, N'American Curl')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (41, 2, N'Angora Turco')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (42, 2, N'Azul Ruso')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (43, 2, N'Balinés')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (44, 2, N'Bengalí')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (45, 2, N'Bombay')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (46, 2, N'Bosque de Noruega')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (47, 2, N'Británico de Pelo Corto')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (48, 2, N'Burmés')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (49, 2, N'Burmilla')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (50, 2, N'Cartujo')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (51, 2, N'Cornish Rex')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (52, 2, N'Devon Rex')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (53, 2, N'Highland Fold')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (54, 2, N'Himalayo')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (55, 2, N'Javanés')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (56, 2, N'Korat')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (57, 2, N'Mau Egipcio')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (58, 2, N'Ocicat')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (59, 2, N'Persa')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (60, 2, N'Ragdoll')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (61, 2, N'Siamés')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (62, 2, N'Siberiano')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (63, 2, N'Van Turco')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (64, 3, N'Perico Australiano')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (65, 3, N'Buitre de Turquía')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (66, 3, N'Pato')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (67, 3, N'Verderón Europeo')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (68, 3, N'Cotorra Argentina')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (69, 3, N'Agapornis Canus')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (70, 3, N'Agapornis Pullarius')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (71, 3, N'Agapornis Taranta')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (72, 3, N'Agapornis Swindernianus')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (73, 3, N'Agapornis Roseicollis')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (74, 3, N'Agapornis Fischeri')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (75, 3, N'Agapornis Personatus')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (76, 3, N'Agapornis Nigrigenis')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (77, 3, N'Forpus')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (78, 3, N'Loro')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (79, 3, N'Turaco')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (80, 3, N'Guacamayo')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (81, 3, N'Paloma')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (82, 3, N'Canario')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (83, 4, N'Killis')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (84, 4, N'Espiga')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (85, 4, N'Anostómido')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (86, 4, N'Gato')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (87, 4, N'Carpas')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (88, 4, N'Mastacembélidos')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (89, 4, N'Gourami besucón')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (90, 4, N'Arcoíris')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (91, 4, N'Percas')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (92, 4, N'Laberíntidos')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (93, 4, N'Guppy')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (94, 4, N'Tetras')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (95, 4, N'Barbos')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (96, 5, N'Dragón barbudo')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (97, 5, N'Gecko leopardo')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (98, 5, N'Camaleón')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (99, 5, N'Iguana verde')
GO
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (100, 5, N'Serpiente del maíz')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (101, 5, N'Pitón bola')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (102, 5, N'Tortuga')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (103, 6, N'Erizo Africano')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (104, 6, N'Erizo Albino')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (105, 6, N'Erizo Egipcio')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (106, 6, N'Hámster Común')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (107, 6, N'Hámster Sirio')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (108, 6, N'Hámster Dorado')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (109, 6, N'Hámster Ruso')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (110, 6, N'Hámster Campbell')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (111, 6, N'Jerbo de Mongolia')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (112, 6, N'Jerbo Duprasi')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (113, 6, N'Rata Manx')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (114, 6, N'Rata Calva')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (115, 6, N'Rata Dumbo')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (116, 6, N'Ratón Chino')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (117, 6, N'Ratón Pigmeo')
INSERT [dbo].[GCP_Raza] ([id_Raza], [id_Especie], [descripcion]) VALUES (118, 6, N'Ratón Cebra')
SET IDENTITY_INSERT [dbo].[GCP_Raza] OFF
SET IDENTITY_INSERT [dbo].[GCP_Sede] ON 

INSERT [dbo].[GCP_Sede] ([id_Sede], [codigo], [nombre], [direccion], [id_Distrito], [telefono]) VALUES (12, N'SECH0001', N'Vet. PetCenter - Chorrillos', N'Av. Mariano Melgar 4751', 6, NULL)
INSERT [dbo].[GCP_Sede] ([id_Sede], [codigo], [nombre], [direccion], [id_Distrito], [telefono]) VALUES (13, N'SELM0002', N'Vet. PetCenter - Molina', N'Av. La Molina 3014', 9, NULL)
INSERT [dbo].[GCP_Sede] ([id_Sede], [codigo], [nombre], [direccion], [id_Distrito], [telefono]) VALUES (14, N'SELI0003', N'Vet. PetCenter - Lince', N'Jr. Santa Patricia 4521 - Alt. CC-RISO', 11, NULL)
INSERT [dbo].[GCP_Sede] ([id_Sede], [codigo], [nombre], [direccion], [id_Distrito], [telefono]) VALUES (15, N'SEMI0004', N'Vet. PetCenter - Miraflores', N'Av. Gregorio Escobedo 123', 13, NULL)
INSERT [dbo].[GCP_Sede] ([id_Sede], [codigo], [nombre], [direccion], [id_Distrito], [telefono]) VALUES (16, N'SELO0005', N'Vet. PetCenter - Los Olivos', N'Av, Angélica Gamarra 2342', 28, NULL)
SET IDENTITY_INSERT [dbo].[GCP_Sede] OFF
SET IDENTITY_INSERT [dbo].[GCP_SedeServicio] ON 

INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (73, 12, 1)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (74, 12, 2)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (75, 12, 3)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (76, 12, 4)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (77, 12, 5)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (78, 12, 9)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (79, 13, 1)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (80, 13, 2)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (81, 13, 3)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (82, 13, 4)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (83, 13, 5)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (84, 13, 6)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (85, 13, 7)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (86, 13, 10)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (87, 14, 1)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (88, 14, 2)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (89, 14, 3)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (90, 14, 4)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (91, 14, 5)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (92, 14, 3)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (93, 14, 4)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (94, 14, 10)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (95, 15, 1)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (96, 15, 2)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (97, 15, 3)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (98, 15, 4)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (99, 15, 5)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (100, 15, 7)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (101, 15, 8)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (102, 15, 9)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (103, 15, 10)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (104, 16, 1)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (105, 16, 2)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (106, 16, 3)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (107, 16, 4)
INSERT [dbo].[GCP_SedeServicio] ([id_SedeServicio], [id_Sede], [id_Servicio]) VALUES (108, 16, 5)
SET IDENTITY_INSERT [dbo].[GCP_SedeServicio] OFF
SET IDENTITY_INSERT [dbo].[GCP_Servicio] ON 

INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (1, N'SACV0001', N'Análisis Clínicos')
INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (2, N'SHOV0002', N'Hospitalización')
INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (3, N'SPEV0003', N'Peluqueria')
INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (4, N'SVAV0004', N'Vacunación')
INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (5, N'SDEV0005', N'Desparasitación')
INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (6, N'SCIV0006', N'Cirugía')
INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (7, N'SSDV0007', N'Salud Dental')
INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (8, N'SECV0008', N'Ecografías')
INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (9, N'SADV0009', N'Atención Domiciliaria')
INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (10, N'SICV0010', N'Implantación de Chips de Identificación')
INSERT [dbo].[GCP_Servicio] ([id_Servicio], [codigo], [descripcion]) VALUES (11, N'SGTV0011', N'Generación de Tarjeta de Identificación')
SET IDENTITY_INSERT [dbo].[GCP_Servicio] OFF
SET IDENTITY_INSERT [dbo].[GCP_TipoCliente] ON 

INSERT [dbo].[GCP_TipoCliente] ([id_TipoCliente], [codigo], [nombre]) VALUES (1, N'TCVP0001', N'Natural')
INSERT [dbo].[GCP_TipoCliente] ([id_TipoCliente], [codigo], [nombre]) VALUES (2, N'TCVP0002', N'Jurídico')
SET IDENTITY_INSERT [dbo].[GCP_TipoCliente] OFF
SET IDENTITY_INSERT [dbo].[GCP_TipoDocumento] ON 

INSERT [dbo].[GCP_TipoDocumento] ([id_TipoDocumento], [codigo], [nombre]) VALUES (1, N'DNI', N'Documento Nacional de Identidad')
INSERT [dbo].[GCP_TipoDocumento] ([id_TipoDocumento], [codigo], [nombre]) VALUES (2, N'RUC', N'Registro Unico del Contribuyente')
SET IDENTITY_INSERT [dbo].[GCP_TipoDocumento] OFF
SET IDENTITY_INSERT [dbo].[GCP_Turno] ON 

INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (1, N'10:00', N'10:30', CAST(0x623C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (2, N'11:00', N'12:00', CAST(0x623C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (3, N'10:00', N'10:30', CAST(0x633C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (4, N'10:40', N'11:10', CAST(0x633C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (5, N'11:20', N'12:50', CAST(0x633C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (6, N'13:00', N'15:00', CAST(0x633C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (7, N'15:00', N'15:30', CAST(0x633C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (8, N'16:00', N'17:30', CAST(0x633C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (9, N'17:40', N'18:50', CAST(0x633C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (10, N'19:00', N'19:40', CAST(0x633C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (11, N'19:30', N'20:00', CAST(0x633C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (12, N'10:00', N'10:30', CAST(0x643C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (13, N'10:40', N'11:10', CAST(0x643C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (14, N'11:20', N'12:50', CAST(0x643C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (15, N'13:00', N'15:00', CAST(0x643C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (16, N'15:00', N'15:30', CAST(0x643C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (17, N'16:00', N'17:30', CAST(0x643C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (18, N'17:40', N'18:50', CAST(0x643C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (19, N'19:00', N'19:40', CAST(0x643C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (20, N'19:30', N'20:00', CAST(0x643C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (21, N'10:00', N'10:30', CAST(0x603C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (22, N'10:40', N'11:10', CAST(0x603C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (23, N'11:20', N'12:50', CAST(0x603C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (24, N'13:00', N'15:00', CAST(0x603C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (25, N'15:00', N'15:30', CAST(0x603C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (26, N'16:00', N'17:30', CAST(0x603C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (27, N'17:40', N'18:50', CAST(0x603C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (28, N'19:00', N'19:40', CAST(0x603C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (29, N'19:30', N'20:00', CAST(0x603C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (30, N'10:00', N'10:30', CAST(0x653C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (31, N'10:40', N'11:10', CAST(0x653C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (32, N'11:20', N'12:50', CAST(0x653C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (33, N'13:00', N'15:00', CAST(0x653C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (34, N'15:00', N'15:30', CAST(0x653C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (35, N'16:00', N'17:30', CAST(0x653C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (36, N'17:40', N'18:50', CAST(0x653C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (37, N'19:00', N'19:40', CAST(0x653C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (38, N'19:30', N'20:00', CAST(0x653C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (39, N'10:00', N'10:30', CAST(0x5F3C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (40, N'10:40', N'11:10', CAST(0x5F3C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (41, N'11:20', N'12:50', CAST(0x5F3C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (42, N'13:00', N'15:00', CAST(0x5F3C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (43, N'15:00', N'15:30', CAST(0x5F3C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (44, N'16:00', N'17:30', CAST(0x5F3C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (45, N'17:40', N'18:50', CAST(0x5F3C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (46, N'19:00', N'19:40', CAST(0x5F3C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (47, N'19:30', N'20:00', CAST(0x5F3C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (48, N'10:00', N'10:30', CAST(0x613C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (49, N'10:40', N'11:10', CAST(0x613C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (50, N'11:20', N'12:50', CAST(0x613C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (51, N'13:00', N'15:00', CAST(0x613C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (52, N'15:00', N'15:30', CAST(0x613C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (53, N'16:00', N'17:30', CAST(0x613C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (54, N'17:40', N'18:50', CAST(0x613C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (55, N'19:00', N'19:40', CAST(0x613C0B00 AS Date))
INSERT [dbo].[GCP_Turno] ([id_Turno], [horaInicio], [horaFin], [fechaTurno]) VALUES (56, N'19:30', N'20:00', CAST(0x613C0B00 AS Date))
SET IDENTITY_INSERT [dbo].[GCP_Turno] OFF
SET IDENTITY_INSERT [dbo].[GHA_Alimento] ON 

INSERT [dbo].[GHA_Alimento] ([Id_Tipo_Alimento], [Descripcion]) VALUES (CAST(1 AS Numeric(6, 0)), N'Galletas Pequeñas')
INSERT [dbo].[GHA_Alimento] ([Id_Tipo_Alimento], [Descripcion]) VALUES (CAST(2 AS Numeric(6, 0)), N'Galletas Medianas')
INSERT [dbo].[GHA_Alimento] ([Id_Tipo_Alimento], [Descripcion]) VALUES (CAST(3 AS Numeric(6, 0)), N'Galletas Grandes')
INSERT [dbo].[GHA_Alimento] ([Id_Tipo_Alimento], [Descripcion]) VALUES (CAST(4 AS Numeric(6, 0)), N'Carnaza Pequeña')
INSERT [dbo].[GHA_Alimento] ([Id_Tipo_Alimento], [Descripcion]) VALUES (CAST(5 AS Numeric(6, 0)), N'Carnaza Mediana')
INSERT [dbo].[GHA_Alimento] ([Id_Tipo_Alimento], [Descripcion]) VALUES (CAST(6 AS Numeric(6, 0)), N'Carnaza Grande')
SET IDENTITY_INSERT [dbo].[GHA_Alimento] OFF
INSERT [dbo].[GHA_Canil] ([Id_Canil], [Tamanio], [TipoEspecie], [Estado], [Descripcion]) VALUES (1, N'Grande', 1, 0, N'Grande Perro')
INSERT [dbo].[GHA_Canil] ([Id_Canil], [Tamanio], [TipoEspecie], [Estado], [Descripcion]) VALUES (2, N'Chico', 1, 0, N'Chico Perro')
INSERT [dbo].[GHA_Canil] ([Id_Canil], [Tamanio], [TipoEspecie], [Estado], [Descripcion]) VALUES (3, N'Mediano', 1, 0, N'Mediano Perro')
INSERT [dbo].[GHA_Canil] ([Id_Canil], [Tamanio], [TipoEspecie], [Estado], [Descripcion]) VALUES (4, N'Grande', 2, 0, N'Grande Gato')
INSERT [dbo].[GHA_Canil] ([Id_Canil], [Tamanio], [TipoEspecie], [Estado], [Descripcion]) VALUES (5, N'Chico', 2, 0, N'Chico Gato')
INSERT [dbo].[GHA_Canil] ([Id_Canil], [Tamanio], [TipoEspecie], [Estado], [Descripcion]) VALUES (6, N'Mediano', 2, 0, N'Mediano Gato')
INSERT [dbo].[GHA_Estado] ([Id_Estado], [Descripcion]) VALUES (N'A', N'Activo')
INSERT [dbo].[GHA_Estado] ([Id_Estado], [Descripcion]) VALUES (N'I', N'Inactivo')
SET IDENTITY_INSERT [dbo].[GHA_Expediente_Hospedaje] ON 

INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (1, N'SRVH2016100002', CAST(0x0000A6FC00C90CC0 AS DateTime), CAST(0x0000A70400C90CC0 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (2, N'SRVH2016100003', CAST(0x0000A6F500000000 AS DateTime), CAST(0x0000A6FD00000000 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (3, N'SRVH2016100004', CAST(0x0000A6F700000000 AS DateTime), CAST(0x0000A6FF00000000 AS DateTime), N'Paciente saludable', 1, CAST(6 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (4, N'SRVH201610005', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), N'Paciente saludable', 1, CAST(8 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (5, N'SRVH2016100006', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), N'Paciente saludable', 1, CAST(1 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (6, N'SRVH2016100007', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (7, N'SRVH2016100008', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (8, N'SRVH2016100009', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), N'Paciente saludable', 1, CAST(6 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (9, N'SRVH2016100010', CAST(0x0000A6FC01499700 AS DateTime), CAST(0x0000A70401499700 AS DateTime), N'Paciente saludable', 1, CAST(8 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (10, N'SRVH2016100011', CAST(0x0000A6FC014DFC00 AS DateTime), CAST(0x0000A704014DFC00 AS DateTime), N'Paciente saludable', 1, CAST(1 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (11, N'SRVH2016100012', CAST(0x0000A6FC008F5F20 AS DateTime), CAST(0x0000A704008F5F20 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (12, N'SRVH2016100013', CAST(0x0000A6FC00B61930 AS DateTime), CAST(0x0000A70400B61930 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (13, N'SRVH2016100014', CAST(0x0000A6FC00B61930 AS DateTime), CAST(0x0000A70400B61930 AS DateTime), N'Paciente saludable', 1, CAST(6 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (14, N'SRVH2016100015', CAST(0x0000A6FC00B61930 AS DateTime), CAST(0x0000A70400B61930 AS DateTime), N'Paciente saludable', 1, CAST(8 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (15, N'SRVH2016100016', CAST(0x0000A6FC00B6A5D0 AS DateTime), CAST(0x0000A70400B6A5D0 AS DateTime), N'Paciente saludable', 1, CAST(1 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (16, N'SRVH2016100017', CAST(0x0000A6FC00B6EC20 AS DateTime), CAST(0x0000A70400B6EC20 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (17, N'SRVH2016100018', CAST(0x0000A6FC00B73270 AS DateTime), CAST(0x0000A70400B73270 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (18, N'SRVH2016100019', CAST(0x0000A6FC00B73270 AS DateTime), CAST(0x0000A70400B73270 AS DateTime), N'Paciente saludable', 1, CAST(6 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (19, N'SRVH2016100020', CAST(0x0000A6FC00B778C0 AS DateTime), CAST(0x0000A70400B778C0 AS DateTime), N'Paciente saludable', 1, CAST(8 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (20, N'SRVH2016100021', CAST(0x0000A6FC00BFFC70 AS DateTime), CAST(0x0000A70400BFFC70 AS DateTime), N'Paciente saludable', 1, CAST(1 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (21, N'SRVH2016100022', CAST(0x0000A6FC00CED150 AS DateTime), CAST(0x0000A70400CED150 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (22, N'SRVH2016100022', CAST(0x0000A6FC00D030E0 AS DateTime), CAST(0x0000A70400D030E0 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (23, N'SRVH2016100023', CAST(0x0000A6FC00D1D6C0 AS DateTime), CAST(0x0000A70400D1D6C0 AS DateTime), N'Paciente saludable', 1, CAST(6 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (24, N'SRVH2016100024', CAST(0x0000A6FC0036A830 AS DateTime), CAST(0x0000A7040036A830 AS DateTime), N'Paciente saludable', 1, CAST(8 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (25, N'SRVH2016100025', CAST(0x0000A6FC003DCC50 AS DateTime), CAST(0x0000A704003DCC50 AS DateTime), N'Paciente saludable', 1, CAST(1 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (26, N'SRVH2016100026', CAST(0x0000A6FC0058FD40 AS DateTime), CAST(0x0000A7040058FD40 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (27, N'SRVH2016100027', CAST(0x0000A6FC005CD5A0 AS DateTime), CAST(0x0000A704005CD5A0 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (28, N'SRVH2016100028', CAST(0x0000A6FC008FEBC0 AS DateTime), CAST(0x0000A704008FEBC0 AS DateTime), N'Paciente saludable', 1, CAST(6 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (29, N'SRVH2016100029', CAST(0x0000A6FC00921E40 AS DateTime), CAST(0x0000A70400921E40 AS DateTime), N'Paciente saludable', 1, CAST(8 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (30, N'SRVH2016100030', CAST(0x0000A6FC00949710 AS DateTime), CAST(0x0000A70400949710 AS DateTime), N'Paciente saludable', 1, CAST(1 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (31, N'SRVH2016100031', CAST(0x0000A6FC009AA1F0 AS DateTime), CAST(0x0000A704009AA1F0 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (32, N'SRVH2016100032', CAST(0x0000A6FC009F9390 AS DateTime), CAST(0x0000A704009F9390 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (33, N'SRVH2016100033', CAST(0x0000A6FC00A6FE00 AS DateTime), CAST(0x0000A70400A6FE00 AS DateTime), N'Paciente saludable', 1, CAST(6 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (34, N'SRVH2016100034', CAST(0x0000A6FC00AB1CB0 AS DateTime), CAST(0x0000A70400AB1CB0 AS DateTime), N'Paciente saludable', 1, CAST(8 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (35, N'SRVH2016100035', CAST(0x0000A6FC00B4FFF0 AS DateTime), CAST(0x0000A70400B4FFF0 AS DateTime), N'Paciente saludable', 1, CAST(1 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (36, N'SRVH2016100036', CAST(0x0000A6FC00BB0AD0 AS DateTime), CAST(0x0000A70400BB0AD0 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (37, N'SRVH2016100037', CAST(0x0000A6FC00BE9CE0 AS DateTime), CAST(0x0000A70400BE9CE0 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (38, N'SRVH2016100038', CAST(0x0000A6FC00C115B0 AS DateTime), CAST(0x0000A70400C115B0 AS DateTime), N'Paciente saludable', 1, CAST(6 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (39, N'SRVH2016100039', CAST(0x0000A6FC00C2BB90 AS DateTime), CAST(0x0000A70400C2BB90 AS DateTime), N'Paciente saludable', 1, CAST(8 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (40, N'SRVH2016100040', CAST(0x0000A6FC00C2BB90 AS DateTime), CAST(0x0000A70400C2BB90 AS DateTime), N'Paciente saludable', 1, CAST(1 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (41, N'SRVH2016100041', CAST(0x0000A6FC00C41B20 AS DateTime), CAST(0x0000A70400C41B20 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (42, N'SRVH2016100042', CAST(0x0000A6FC00C693F0 AS DateTime), CAST(0x0000A70400C693F0 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (43, N'SRVH2016100043', CAST(0x0000A6FC00C6DA40 AS DateTime), CAST(0x0000A70400C6DA40 AS DateTime), N'Paciente saludable', 1, CAST(6 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (44, N'SRVH2016100044', CAST(0x0000A6FC00C766E0 AS DateTime), CAST(0x0000A70400C766E0 AS DateTime), N'Paciente saludable', 1, CAST(8 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (45, N'SRVH2016100045', CAST(0x0000A6FC00C7F380 AS DateTime), CAST(0x0000A70400C7F380 AS DateTime), N'Paciente saludable', 1, CAST(1 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (46, N'SRVH2016100046', CAST(0x0000A6FC00C99960 AS DateTime), CAST(0x0000A70400C99960 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (47, N'SRVH2016100047', CAST(0x0000A6FC00CAF8F0 AS DateTime), CAST(0x0000A70400CAF8F0 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (48, N'SRVH2016100048', CAST(0x0000A6FC00CB8590 AS DateTime), CAST(0x0000A70400CB8590 AS DateTime), N'Paciente saludable', 1, CAST(6 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (49, N'SRVH2016100049', CAST(0x0000A6FC00CC1230 AS DateTime), CAST(0x0000A70400CC1230 AS DateTime), N'Paciente saludable', 1, CAST(8 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (50, N'SRVH2016100050', CAST(0x0000A6FC00CC9ED0 AS DateTime), CAST(0x0000A70400CC9ED0 AS DateTime), N'Paciente saludable', 1, CAST(1 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (51, N'SRVH2016100051', CAST(0x0000A6FC00CDFE60 AS DateTime), CAST(0x0000A70400CDFE60 AS DateTime), N'Paciente saludable', 1, CAST(2 AS Numeric(10, 0)))
INSERT [dbo].[GHA_Expediente_Hospedaje] ([id_ExpHosp], [Codigo_Expediente], [Fecha_Hospedaje], [Fecha_Salida], [Observacion], [Estado], [Id_Mascota]) VALUES (52, N'SRVH2016100052', CAST(0x0000A6FC00441D80 AS DateTime), CAST(0x0000A70400441D80 AS DateTime), N'Paciente saludable', 1, CAST(3 AS Numeric(10, 0)))
SET IDENTITY_INSERT [dbo].[GHA_Expediente_Hospedaje] OFF
SET IDENTITY_INSERT [dbo].[GHA_Objetivo_Plan] ON 

INSERT [dbo].[GHA_Objetivo_Plan] ([Id_Objetivo], [Descripcion]) VALUES (CAST(1 AS Numeric(10, 0)), N'Bajar de Peso')
INSERT [dbo].[GHA_Objetivo_Plan] ([Id_Objetivo], [Descripcion]) VALUES (CAST(2 AS Numeric(10, 0)), N'Subir de Peso')
INSERT [dbo].[GHA_Objetivo_Plan] ([Id_Objetivo], [Descripcion]) VALUES (CAST(3 AS Numeric(10, 0)), N'Regular')
INSERT [dbo].[GHA_Objetivo_Plan] ([Id_Objetivo], [Descripcion]) VALUES (CAST(4 AS Numeric(10, 0)), N'Especial')
SET IDENTITY_INSERT [dbo].[GHA_Objetivo_Plan] OFF
SET IDENTITY_INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ON 

INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (16, 1, CAST(1 AS Numeric(10, 0)), N'PLAL201610001', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A003BB7F3 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (17, 3, CAST(1 AS Numeric(10, 0)), N'PLAL201610002', CAST(0x0000A5C500000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A003D9371 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (18, 1, CAST(1 AS Numeric(10, 0)), N'PLAL201610003', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A003FE548 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (19, 1, CAST(1 AS Numeric(10, 0)), N'PLAL201610004', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A00404BF8 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (20, 1, CAST(1 AS Numeric(10, 0)), N'PLAL201610005', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A0040BAF7 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (21, 1, CAST(1 AS Numeric(10, 0)), N'PLAL201610006', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A0041709C AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (22, 1, CAST(1 AS Numeric(10, 0)), N'PLAL201610007', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A0041CB43 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (23, 1, CAST(1 AS Numeric(10, 0)), N'PLAL201610008', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A0042926F AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (24, 1, CAST(2 AS Numeric(10, 0)), N'PLAL201610009', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A0042C4B3 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (25, 1, CAST(2 AS Numeric(10, 0)), N'PLAL2016100010', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A00432A09 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (26, 1, CAST(2 AS Numeric(10, 0)), N'PLAL2016100011', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A004348CB AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (27, 1, CAST(2 AS Numeric(10, 0)), N'PLAL2016100012', CAST(0x0000A58900000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A00436FC3 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (28, 1, CAST(3 AS Numeric(10, 0)), N'PLAL2016100013', CAST(0x0000A69200000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A0043A7B3 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (29, 9, CAST(1 AS Numeric(10, 0)), N'PLAL2016100000000000000014', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69D00000000 AS DateTime), CAST(0x0000A69A013A82CA AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (30, 9, CAST(1 AS Numeric(10, 0)), N'PLAL2016100014', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69D00000000 AS DateTime), CAST(0x0000A69A013B15E7 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (32, 11, CAST(1 AS Numeric(10, 0)), N'PLAL2016100015', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A0150EB54 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (33, 11, CAST(1 AS Numeric(10, 0)), N'PLAL2016100016', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A0151A4CA AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (34, 11, CAST(1 AS Numeric(10, 0)), N'PLAL2016100017', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A017403F3 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (35, 11, CAST(1 AS Numeric(10, 0)), N'PLAL2016100018', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A0174DAE0 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (36, 11, CAST(1 AS Numeric(10, 0)), N'PLAL2016100019', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A0174EE01 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (37, 11, CAST(1 AS Numeric(10, 0)), N'PLAL2016100020', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69A017AF0BF AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (38, 21, CAST(1 AS Numeric(10, 0)), N'PLAL2016100021', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A6A000000000 AS DateTime), CAST(0x0000A69A0186CF20 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (39, 21, CAST(2 AS Numeric(10, 0)), N'PLAL2016100022', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A6A000000000 AS DateTime), CAST(0x0000A69B00074E7F AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (40, 24, CAST(1 AS Numeric(10, 0)), N'PLAL2016100023', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A69B000E0495 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (41, 53, CAST(1 AS Numeric(10, 0)), N'PLAL2016100024', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A6A000000000 AS DateTime), CAST(0x0000A69D000DB6C2 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (42, 53, CAST(1 AS Numeric(10, 0)), N'PLAL2016100025', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A6A000000000 AS DateTime), CAST(0x0000A69D0102ED58 AS DateTime), N'I')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (43, 53, CAST(1 AS Numeric(10, 0)), N'PLAL2016100026', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A6A000000000 AS DateTime), CAST(0x0000A69D0107C680 AS DateTime), N'A')
INSERT [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan], [Id_Servicio], [Id_Objetivo], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (44, 56, CAST(1 AS Numeric(10, 0)), N'PLAL2016100027', CAST(0x0000A69B00000000 AS DateTime), CAST(0x0000A6A000000000 AS DateTime), CAST(0x0000A69D010AAA2D AS DateTime), N'A')
SET IDENTITY_INSERT [dbo].[GHA_Plan_Alimenticio_Cab] OFF
SET IDENTITY_INSERT [dbo].[GHA_Plan_Alimenticio_Det] ON 

INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (298, 28, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69200000000 AS DateTime), N'12                  ', CAST(2 AS Numeric(6, 0)), CAST(234.00 AS Decimal(16, 2)), N'fdsdg')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (299, 28, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A69300000000 AS DateTime), N'23                  ', CAST(2 AS Numeric(6, 0)), CAST(234.00 AS Decimal(16, 2)), N'sdfsgf')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (300, 28, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A69400000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (301, 28, CAST(4 AS Numeric(3, 0)), NULL, CAST(0x0000A69500000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (302, 28, CAST(5 AS Numeric(3, 0)), NULL, CAST(0x0000A69600000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (303, 28, CAST(6 AS Numeric(3, 0)), NULL, CAST(0x0000A69700000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (304, 28, CAST(7 AS Numeric(3, 0)), NULL, CAST(0x0000A69800000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (305, 28, CAST(8 AS Numeric(3, 0)), NULL, CAST(0x0000A69900000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (306, 28, CAST(9 AS Numeric(3, 0)), NULL, CAST(0x0000A69A00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (307, 28, CAST(10 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (308, 17, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69400000000 AS DateTime), N'12:15               ', CAST(1 AS Numeric(6, 0)), CAST(23.50 AS Decimal(16, 2)), N'no')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (309, 17, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69400000000 AS DateTime), N'13:15               ', CAST(1 AS Numeric(6, 0)), CAST(45.50 AS Decimal(16, 2)), N'on')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (310, 17, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A69500000000 AS DateTime), N'15:18               ', CAST(1 AS Numeric(6, 0)), CAST(120.00 AS Decimal(16, 2)), N'obser')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (311, 17, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A69600000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (312, 17, CAST(4 AS Numeric(3, 0)), NULL, CAST(0x0000A69700000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (313, 17, CAST(5 AS Numeric(3, 0)), NULL, CAST(0x0000A69800000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (314, 17, CAST(6 AS Numeric(3, 0)), NULL, CAST(0x0000A69900000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (315, 17, CAST(7 AS Numeric(3, 0)), NULL, CAST(0x0000A69A00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (316, 17, CAST(8 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (317, 29, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), N'12:12               ', CAST(1 AS Numeric(6, 0)), CAST(12.00 AS Decimal(16, 2)), N'23')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (318, 29, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A69C00000000 AS DateTime), N'12:12               ', CAST(1 AS Numeric(6, 0)), CAST(122.00 AS Decimal(16, 2)), N'fddf')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (319, 29, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A69D00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (323, 32, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (325, 33, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), N'00:12               ', CAST(1 AS Numeric(6, 0)), CAST(15.00 AS Decimal(16, 2)), N'')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (326, 34, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (327, 35, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (329, 30, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), N'00:12               ', CAST(1 AS Numeric(6, 0)), CAST(12.00 AS Decimal(16, 2)), N'')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (330, 30, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A69C00000000 AS DateTime), N'00:12               ', CAST(3 AS Numeric(6, 0)), CAST(12.00 AS Decimal(16, 2)), N'123123')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (331, 30, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A69D00000000 AS DateTime), N'01:13               ', CAST(1 AS Numeric(6, 0)), CAST(12.00 AS Decimal(16, 2)), N'123123')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (332, 36, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (333, 37, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (334, 38, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), N'00:12               ', CAST(1 AS Numeric(6, 0)), CAST(123.00 AS Decimal(16, 2)), N'')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (335, 38, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), N'14:14               ', CAST(3 AS Numeric(6, 0)), CAST(12.00 AS Decimal(16, 2)), N'')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (336, 38, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A69C00000000 AS DateTime), N'12:12               ', CAST(1 AS Numeric(6, 0)), CAST(12.00 AS Decimal(16, 2)), N'')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (337, 38, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A69D00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (338, 38, CAST(4 AS Numeric(3, 0)), NULL, CAST(0x0000A69E00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (339, 38, CAST(5 AS Numeric(3, 0)), NULL, CAST(0x0000A69F00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (340, 38, CAST(6 AS Numeric(3, 0)), NULL, CAST(0x0000A6A000000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (353, 39, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), N'15:12               ', CAST(1 AS Numeric(6, 0)), CAST(12.00 AS Decimal(16, 2)), N'observacion')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (354, 39, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A69C00000000 AS DateTime), N'12:12               ', CAST(1 AS Numeric(6, 0)), CAST(21.00 AS Decimal(16, 2)), N'12')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (355, 39, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A69D00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (356, 39, CAST(4 AS Numeric(3, 0)), NULL, CAST(0x0000A69E00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (357, 39, CAST(5 AS Numeric(3, 0)), NULL, CAST(0x0000A69F00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (358, 39, CAST(6 AS Numeric(3, 0)), NULL, CAST(0x0000A6A000000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (359, 40, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), N'00:12               ', CAST(1 AS Numeric(6, 0)), CAST(12.00 AS Decimal(16, 2)), N'')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (378, 41, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (379, 41, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A69C00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (380, 41, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A69D00000000 AS DateTime), N'15:50               ', CAST(2 AS Numeric(6, 0)), CAST(350.00 AS Decimal(16, 2)), N'')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (381, 41, CAST(4 AS Numeric(3, 0)), NULL, CAST(0x0000A69E00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (382, 41, CAST(5 AS Numeric(3, 0)), NULL, CAST(0x0000A69F00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (383, 41, CAST(6 AS Numeric(3, 0)), NULL, CAST(0x0000A6A000000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (412, 42, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (413, 42, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A69C00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (414, 42, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A69D00000000 AS DateTime), N'16:50               ', CAST(2 AS Numeric(6, 0)), CAST(67.00 AS Decimal(16, 2)), N'')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (415, 42, CAST(4 AS Numeric(3, 0)), NULL, CAST(0x0000A69E00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (416, 42, CAST(5 AS Numeric(3, 0)), NULL, CAST(0x0000A69F00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (417, 42, CAST(6 AS Numeric(3, 0)), NULL, CAST(0x0000A6A000000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (418, 43, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), N'12:00               ', CAST(1 AS Numeric(6, 0)), CAST(150.00 AS Decimal(16, 2)), N'')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (419, 43, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A69C00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (420, 43, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A69D00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (421, 43, CAST(4 AS Numeric(3, 0)), NULL, CAST(0x0000A69E00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (422, 43, CAST(5 AS Numeric(3, 0)), NULL, CAST(0x0000A69F00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (423, 43, CAST(6 AS Numeric(3, 0)), NULL, CAST(0x0000A6A000000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (424, 44, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A69B00000000 AS DateTime), N'12:00               ', CAST(1 AS Numeric(6, 0)), CAST(150.00 AS Decimal(16, 2)), N'')
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (425, 44, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A69C00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (426, 44, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A69D00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (427, 44, CAST(4 AS Numeric(3, 0)), NULL, CAST(0x0000A69E00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (428, 44, CAST(5 AS Numeric(3, 0)), NULL, CAST(0x0000A69F00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
INSERT [dbo].[GHA_Plan_Alimenticio_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Alimento], [Porcion], [Observacion]) VALUES (429, 44, CAST(6 AS Numeric(3, 0)), NULL, CAST(0x0000A6A000000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), CAST(0.00 AS Decimal(16, 2)), NULL)
SET IDENTITY_INSERT [dbo].[GHA_Plan_Alimenticio_Det] OFF
SET IDENTITY_INSERT [dbo].[GHA_Plan_Rutina_Cab] ON 

INSERT [dbo].[GHA_Plan_Rutina_Cab] ([Id_Plan], [Id_Servicio], [Nombre], [Fecha_Inicio], [Fecha_Fin], [Fecha_Creacion], [Estado]) VALUES (5, 5, N'PLRD2017010001', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), CAST(0x0000A6FF01770166 AS DateTime), N'A')
SET IDENTITY_INSERT [dbo].[GHA_Plan_Rutina_Cab] OFF
SET IDENTITY_INSERT [dbo].[GHA_Plan_Rutina_Det] ON 

INSERT [dbo].[GHA_Plan_Rutina_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Rutina], [Observacion]) VALUES (37, 5, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A6FC00000000 AS DateTime), N'10:30               ', CAST(2 AS Numeric(6, 0)), N'dddd')
INSERT [dbo].[GHA_Plan_Rutina_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Rutina], [Observacion]) VALUES (38, 5, CAST(1 AS Numeric(3, 0)), NULL, CAST(0x0000A6FC00000000 AS DateTime), N'16:00               ', CAST(1 AS Numeric(6, 0)), N'')
INSERT [dbo].[GHA_Plan_Rutina_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Rutina], [Observacion]) VALUES (39, 5, CAST(2 AS Numeric(3, 0)), NULL, CAST(0x0000A6FD00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), NULL)
INSERT [dbo].[GHA_Plan_Rutina_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Rutina], [Observacion]) VALUES (40, 5, CAST(3 AS Numeric(3, 0)), NULL, CAST(0x0000A6FE00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), NULL)
INSERT [dbo].[GHA_Plan_Rutina_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Rutina], [Observacion]) VALUES (41, 5, CAST(4 AS Numeric(3, 0)), NULL, CAST(0x0000A6FF00000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), NULL)
INSERT [dbo].[GHA_Plan_Rutina_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Rutina], [Observacion]) VALUES (42, 5, CAST(5 AS Numeric(3, 0)), NULL, CAST(0x0000A70000000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), NULL)
INSERT [dbo].[GHA_Plan_Rutina_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Rutina], [Observacion]) VALUES (43, 5, CAST(6 AS Numeric(3, 0)), NULL, CAST(0x0000A70100000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), NULL)
INSERT [dbo].[GHA_Plan_Rutina_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Rutina], [Observacion]) VALUES (44, 5, CAST(7 AS Numeric(3, 0)), NULL, CAST(0x0000A70200000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), NULL)
INSERT [dbo].[GHA_Plan_Rutina_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Rutina], [Observacion]) VALUES (45, 5, CAST(8 AS Numeric(3, 0)), NULL, CAST(0x0000A70300000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), NULL)
INSERT [dbo].[GHA_Plan_Rutina_Det] ([Id_Det], [Id_Plan], [Id_Secuencia], [Nombre_Plan], [Fecha_Aplicacion], [Hora_Aplicacion], [Id_Tipo_Rutina], [Observacion]) VALUES (46, 5, CAST(9 AS Numeric(3, 0)), NULL, CAST(0x0000A70400000000 AS DateTime), NULL, CAST(0 AS Numeric(6, 0)), NULL)
SET IDENTITY_INSERT [dbo].[GHA_Plan_Rutina_Det] OFF
INSERT [dbo].[GHA_Reserva_Cita] ([Id_Reserva], [Cod_Reserva], [tipo_Servicio], [Fec_Inicio], [Fec_Fin], [Observacion], [Id_Mascota], [estado]) VALUES (1, N'RSV2016100001', N'Hospedaje', CAST(0xF63B0B00 AS Date), CAST(0xF63B0B00 AS Date), N'OBSERVACION', CAST(1 AS Numeric(10, 0)), 0)
INSERT [dbo].[GHA_Reserva_Cita] ([Id_Reserva], [Cod_Reserva], [tipo_Servicio], [Fec_Inicio], [Fec_Fin], [Observacion], [Id_Mascota], [estado]) VALUES (2, N'RSV2016100002', N'Hospedaje', CAST(0xF63B0B00 AS Date), CAST(0xF63B0B00 AS Date), N'OBSERVACION', CAST(2 AS Numeric(10, 0)), 0)
INSERT [dbo].[GHA_Reserva_Cita] ([Id_Reserva], [Cod_Reserva], [tipo_Servicio], [Fec_Inicio], [Fec_Fin], [Observacion], [Id_Mascota], [estado]) VALUES (3, N'RSV2016100003', N'Hospedaje', CAST(0xF63B0B00 AS Date), CAST(0xFB3B0B00 AS Date), N'OBSERVACION', CAST(3 AS Numeric(10, 0)), 0)
INSERT [dbo].[GHA_Reserva_Cita] ([Id_Reserva], [Cod_Reserva], [tipo_Servicio], [Fec_Inicio], [Fec_Fin], [Observacion], [Id_Mascota], [estado]) VALUES (4, N'RSV2016000004', N'Hospedaje', CAST(0xF63B0B00 AS Date), CAST(0xFB3B0B00 AS Date), N'OBSERVACION', CAST(8 AS Numeric(10, 0)), 0)
INSERT [dbo].[GHA_Reserva_Cita] ([Id_Reserva], [Cod_Reserva], [tipo_Servicio], [Fec_Inicio], [Fec_Fin], [Observacion], [Id_Mascota], [estado]) VALUES (5, N'RSV2016090001', N'Hospedaje', CAST(0xD83B0B00 AS Date), CAST(0xDD3B0B00 AS Date), N'ob', CAST(1 AS Numeric(10, 0)), 0)
INSERT [dbo].[GHA_Reserva_Cita] ([Id_Reserva], [Cod_Reserva], [tipo_Servicio], [Fec_Inicio], [Fec_Fin], [Observacion], [Id_Mascota], [estado]) VALUES (6, N'RSV2016000005', N'Hospedaje', CAST(0xF63B0B00 AS Date), CAST(0xFB3B0B00 AS Date), N'OBSERVACION', CAST(8 AS Numeric(10, 0)), 0)
SET IDENTITY_INSERT [dbo].[GHA_Revision_Medica] ON 

INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (3, CAST(0x0000A69B00000000 AS DateTime), N'Observaciones:	', N'Recomendaciones', 1, 2)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (4, CAST(0x0000A69B00000000 AS DateTime), N'observacion', N'Recomendaciones', 0, 8)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (5, CAST(0x0000A69B00000000 AS DateTime), N'ninguna', N'ninguna', 1, 9)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (6, CAST(0x0000A69A00000000 AS DateTime), N'asdasf', N'asdasdasd', 1, 10)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (7, CAST(0x0000A69A00000000 AS DateTime), N'dfgfdg', N'dfgdfg', 1, 11)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (8, CAST(0x0000A69A00000000 AS DateTime), N'', N'', 1, 12)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (9, CAST(0x0000A69A00000000 AS DateTime), N'asdas', N'asfasf', 1, 21)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (10, CAST(0x0000A69B00000000 AS DateTime), N'123', N'234423', 1, 22)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (11, CAST(0x0000A69B00000000 AS DateTime), N'Observaciones', N'Recomendaciones', 1, 23)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (12, CAST(0x0000A69B00000000 AS DateTime), N'adsasd', N'', 1, 24)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (13, CAST(0x0000A69B00000000 AS DateTime), N'obs', N'rec', 0, 28)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (14, CAST(0x0000A69B00000000 AS DateTime), N'obs', N'rec', 1, 30)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (15, CAST(0x0000A69B00000000 AS DateTime), N'obs', N'rec', 0, 31)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (16, CAST(0x0000A69D00000000 AS DateTime), N'vff', N'fff', 0, 34)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (17, CAST(0x0000A69B00000000 AS DateTime), N'', N'', 1, 35)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (18, CAST(0x0000A69B00000000 AS DateTime), N'obs', N'rec', 1, 36)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (19, CAST(0x0000A69B00000000 AS DateTime), N'', N'', 1, 37)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (20, CAST(0x0000A69B00000000 AS DateTime), N'tt', N'tt', 0, 38)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (21, CAST(0x0000A69C00000000 AS DateTime), N'asdas', N'dasdasd', 0, 41)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (22, CAST(0x0000A69C00000000 AS DateTime), N'fffffff', N'recsddd', 1, 42)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (23, CAST(0x0000A69C00000000 AS DateTime), N'er', N'er', 1, 45)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (24, CAST(0x0000A69D00000000 AS DateTime), N'aa', N'bbb', 1, 48)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (25, CAST(0x0000A69D00000000 AS DateTime), N'onbss', N'obs', 0, 49)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (26, CAST(0x0000A69D00000000 AS DateTime), N'eee', N'rrr', 0, 50)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (27, CAST(0x0000A69D00000000 AS DateTime), N'dd', N'd', 0, 51)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (28, CAST(0x0000A69D00000000 AS DateTime), N'..', N'll', 1, 52)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (29, CAST(0x0000A69D00000000 AS DateTime), N'', N'', 1, 53)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (30, CAST(0x0000A69D00000000 AS DateTime), N'', N'', 1, 55)
INSERT [dbo].[GHA_Revision_Medica] ([id_Revision], [Fecha_Revision], [Observacion], [Recomendacion], [Resultado], [Id_Servicio]) VALUES (31, CAST(0x0000A69D00000000 AS DateTime), N'', N'', 1, 56)
SET IDENTITY_INSERT [dbo].[GHA_Revision_Medica] OFF
SET IDENTITY_INSERT [dbo].[GHA_ServicioHospedaje] ON 

INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (1, N'SRVH2016100001', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), NULL, N'T', 1, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (2, N'SRVH2016100002', CAST(0x0000A6FC00C90CC0 AS DateTime), CAST(0x0000A70400C90CC0 AS DateTime), N'Observaciones', N'T', 2, 1, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (3, N'SRVH2016100003', CAST(0x0000A6F500000000 AS DateTime), CAST(0x0000A6FD00000000 AS DateTime), N'', N'T', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (4, N'SRVH2016100004', CAST(0x0000A6F700000000 AS DateTime), CAST(0x0000A6FF00000000 AS DateTime), NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (5, N'SRVH201610005', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), NULL, N'A', 1, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (6, N'SRVH2016100006', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), N'3222334234', N'T', 1, 1, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (7, N'SRVH2016100007', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), N'', N'T', 1, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (8, N'SRVH2016100008', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), N'', N'R', 1, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (9, N'SRVH2016100009', CAST(0x0000A6FC00000000 AS DateTime), CAST(0x0000A70400000000 AS DateTime), N'TODO BIEN', N'A', 3, 1, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (10, N'SRVH2016100010', CAST(0x0000A6FC01499700 AS DateTime), CAST(0x0000A70401499700 AS DateTime), N'todo bien', N'T', 1, 2, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (11, N'SRVH2016100011', CAST(0x0000A6FC014DFC00 AS DateTime), CAST(0x0000A704014DFC00 AS DateTime), N'', N'C', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (12, N'SRVH2016100012', CAST(0x0000A6FC008F5F20 AS DateTime), CAST(0x0000A704008F5F20 AS DateTime), N'', N'A', 1, 1, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (13, N'SRVH2016100013', CAST(0x0000A6FC00B61930 AS DateTime), CAST(0x0000A70400B61930 AS DateTime), N'', N'P', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (14, N'SRVH2016100014', CAST(0x0000A6FC00B61930 AS DateTime), CAST(0x0000A70400B61930 AS DateTime), N'', N'P', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (15, N'SRVH2016100015', CAST(0x0000A6FC00B61930 AS DateTime), CAST(0x0000A70400B61930 AS DateTime), N'', N'P', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (16, N'SRVH2016100016', CAST(0x0000A6FC00B6A5D0 AS DateTime), CAST(0x0000A70400B6A5D0 AS DateTime), N'', N'P', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (17, N'SRVH2016100017', CAST(0x0000A6FC00B6EC20 AS DateTime), CAST(0x0000A70400B6EC20 AS DateTime), N'', N'P', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (18, N'SRVH2016100018', CAST(0x0000A6FC00B73270 AS DateTime), CAST(0x0000A70400B73270 AS DateTime), N'', N'P', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (19, N'SRVH2016100019', CAST(0x0000A6FC00B73270 AS DateTime), CAST(0x0000A70400B73270 AS DateTime), N'', N'P', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (20, N'SRVH2016100020', CAST(0x0000A6FC00B778C0 AS DateTime), CAST(0x0000A70400B778C0 AS DateTime), N'', N'P', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (21, N'SRVH2016100021', CAST(0x0000A6FC00BFFC70 AS DateTime), CAST(0x0000A70400BFFC70 AS DateTime), N'', N'A', 3, 1, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (22, N'SRVH2016100022', CAST(0x0000A6FC00CED150 AS DateTime), CAST(0x0000A70400CED150 AS DateTime), N'ASDASD', N'T', 3, 1, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (23, N'SRVH2016100022', CAST(0x0000A6FC00D030E0 AS DateTime), CAST(0x0000A70400D030E0 AS DateTime), N'', N'T', 1, 3, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (24, N'SRVH2016100023', CAST(0x0000A6FC00D1D6C0 AS DateTime), CAST(0x0000A70400D1D6C0 AS DateTime), N'', N'A', 2, 1, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (28, N'SRVH2016100024', CAST(0x0000A6FC0036A830 AS DateTime), CAST(0x0000A7040036A830 AS DateTime), N'observaciones', N'T', 4, 1, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (29, N'SRVH2016100025', CAST(0x0000A6FC003DCC50 AS DateTime), CAST(0x0000A704003DCC50 AS DateTime), N'', N'P', 4, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (30, N'SRVH2016100026', CAST(0x0000A6FC0058FD40 AS DateTime), CAST(0x0000A7040058FD40 AS DateTime), N'obs', N'A', 4, 3, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (31, N'SRVH2016100027', CAST(0x0000A6FC005CD5A0 AS DateTime), CAST(0x0000A704005CD5A0 AS DateTime), N'obs', N'T', 4, 2, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (32, N'SRVH2016100028', CAST(0x0000A6FC008FEBC0 AS DateTime), CAST(0x0000A704008FEBC0 AS DateTime), N'', N'P', 4, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (33, N'SRVH2016100029', CAST(0x0000A6FC00921E40 AS DateTime), CAST(0x0000A70400921E40 AS DateTime), N'obs', N'P', 4, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (34, N'SRVH2016100030', CAST(0x0000A6FC00949710 AS DateTime), CAST(0x0000A70400949710 AS DateTime), N'', N'T', 4, 3, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (35, N'SRVH2016100031', CAST(0x0000A6FC009AA1F0 AS DateTime), CAST(0x0000A704009AA1F0 AS DateTime), N'', N'A', 4, 6, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (36, N'SRVH2016100032', CAST(0x0000A6FC009F9390 AS DateTime), CAST(0x0000A704009F9390 AS DateTime), N'obs', N'A', 4, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (37, N'SRVH2016100033', CAST(0x0000A6FC00A6FE00 AS DateTime), CAST(0x0000A70400A6FE00 AS DateTime), N'fff', N'T', 4, 3, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (38, N'SRVH2016100034', CAST(0x0000A6FC00AB1CB0 AS DateTime), CAST(0x0000A70400AB1CB0 AS DateTime), N'dc', N'T', 4, 1, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (39, N'SRVH2016100035', CAST(0x0000A6FC00B4FFF0 AS DateTime), CAST(0x0000A70400B4FFF0 AS DateTime), N'obs', N'P', 4, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (40, N'SRVH2016100036', CAST(0x0000A6FC00BB0AD0 AS DateTime), CAST(0x0000A70400BB0AD0 AS DateTime), N'fffg', N'P', 4, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (41, N'SRVH2016100037', CAST(0x0000A6FC00BE9CE0 AS DateTime), CAST(0x0000A70400BE9CE0 AS DateTime), N'', N'T', 4, 1, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (42, N'SRVH2016100038', CAST(0x0000A6FC00C115B0 AS DateTime), CAST(0x0000A70400C115B0 AS DateTime), N'', N'T', 4, 5, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (43, N'SRVH2016100039', CAST(0x0000A6FC00C2BB90 AS DateTime), CAST(0x0000A70400C2BB90 AS DateTime), N'', N'P', 4, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (44, N'SRVH2016100040', CAST(0x0000A6FC00C2BB90 AS DateTime), CAST(0x0000A70400C2BB90 AS DateTime), N'', N'P', 1, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (45, N'SRVH2016100041', CAST(0x0000A6FC00C41B20 AS DateTime), CAST(0x0000A70400C41B20 AS DateTime), N'', N'T', 4, 1, 0)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (46, N'SRVH2016100042', CAST(0x0000A6FC00C693F0 AS DateTime), CAST(0x0000A70400C693F0 AS DateTime), N'', N'P', 1, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (47, N'SRVH2016100043', CAST(0x0000A6FC00C6DA40 AS DateTime), CAST(0x0000A70400C6DA40 AS DateTime), N'', N'P', 1, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (48, N'SRVH2016100044', CAST(0x0000A6FC00C766E0 AS DateTime), CAST(0x0000A70400C766E0 AS DateTime), N'la 4 es del registro que ves en la pantalla anterior.. por eso no le puedes crear otro servicio
oki', N'C', 1, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (49, N'SRVH2016100045', CAST(0x0000A6FC00C7F380 AS DateTime), CAST(0x0000A70400C7F380 AS DateTime), N'OK', N'T', 1, 1, 0)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (50, N'SRVH2016100046', CAST(0x0000A6FC00C99960 AS DateTime), CAST(0x0000A70400C99960 AS DateTime), N'', N'R', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (51, N'SRVH2016100047', CAST(0x0000A6FC00CAF8F0 AS DateTime), CAST(0x0000A70400CAF8F0 AS DateTime), N'obseeee', N'R', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (52, N'SRVH2016100048', CAST(0x0000A6FC00CB8590 AS DateTime), CAST(0x0000A70400CB8590 AS DateTime), N'dced', N'C', 3, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (53, N'SRVH2016100049', CAST(0x0000A6FC00CC1230 AS DateTime), CAST(0x0000A70400CC1230 AS DateTime), N'', N'A', 3, 1, 0)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (54, N'SRVH2016100050', CAST(0x0000A6FC00CC9ED0 AS DateTime), CAST(0x0000A70400CC9ED0 AS DateTime), N'', N'P', 2, NULL, 1)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (55, N'SRVH2016100051', CAST(0x0000A6FC00CDFE60 AS DateTime), CAST(0x0000A70400CDFE60 AS DateTime), N'', N'T', 2, 3, 0)
INSERT [dbo].[GHA_ServicioHospedaje] ([Id_Servicio], [Codigo_Expediente], [Fecha_Ingreso], [Fecha_Salida], [Observacion], [Estado], [Id_Reserva], [Id_Canil], [AuditEstado]) VALUES (56, N'SRVH2016100052', CAST(0x0000A6FC00441D80 AS DateTime), CAST(0x0000A70400441D80 AS DateTime), N'', N'C', 6, NULL, 0)
SET IDENTITY_INSERT [dbo].[GHA_ServicioHospedaje] OFF
INSERT [dbo].[GHA_Tipo_Rutina] ([id_TipoRutina], [Descripcion]) VALUES (1, N'Paseo en Zona Comunitaria')
INSERT [dbo].[GHA_Tipo_Rutina] ([id_TipoRutina], [Descripcion]) VALUES (2, N'Visita Médica')
INSERT [dbo].[GHA_Tipo_Rutina] ([id_TipoRutina], [Descripcion]) VALUES (3, N'Visita Nutricionista')
INSERT [dbo].[GHA_Tipo_Rutina] ([id_TipoRutina], [Descripcion]) VALUES (4, N'Paseo en Zona Privada')
INSERT [dbo].[GHA_Tipo_Rutina] ([id_TipoRutina], [Descripcion]) VALUES (5, N'Otros')
SET IDENTITY_INSERT [dbo].[GPC_Proveedor] ON 

INSERT [dbo].[GPC_Proveedor] ([idProveedor], [Codigo], [TipoDocumento], [Documento], [RazonSocial], [Direccion], [Telefono], [Contacto], [Tipo], [Puntaje], [ESTADO]) VALUES (3, N'3', N'2', N'10203040501', N'MASCOTAS CORP', N'AV. EL TREBOL 5565', N'987654321', N'MANUEL FERNANDO', N'-', 50, N'ACT')
INSERT [dbo].[GPC_Proveedor] ([idProveedor], [Codigo], [TipoDocumento], [Documento], [RazonSocial], [Direccion], [Telefono], [Contacto], [Tipo], [Puntaje], [ESTADO]) VALUES (4, N'4', N'2', N'12345678910', N'FARMACEUTICA DOG''', N'54545', N'2224456"', N'juan perez', N'-', 50, N'ACT')
INSERT [dbo].[GPC_Proveedor] ([idProveedor], [Codigo], [TipoDocumento], [Documento], [RazonSocial], [Direccion], [Telefono], [Contacto], [Tipo], [Puntaje], [ESTADO]) VALUES (5, N'5', N'2', N'234167894567', N'PET SHOP 2', N'AV. MONTERRICO 34567', N'23456', N'', N'-', 50, N'INA')
SET IDENTITY_INSERT [dbo].[GPC_Proveedor] OFF
INSERT [dbo].[SEG_Opcion] ([CO_OPCION], [NO_OPCION], [RT_OPCION], [ST_REGISTRO], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI], [CO_OPCION_PADRE], [CO_APLICACION], [CO_NIVEL], [TI_OPEN], [IMG_OPCION], [DES_OPCION], [TI_PAR_RUTA], [AB_OPCION], [NU_ORDEN]) VALUES (CAST(1 AS Numeric(18, 0)), N'Módulo de Compras', N'', CAST(1 AS Numeric(1, 0)), N'00000000000001', CAST(0x0000A695000172C2 AS DateTime), N'00000000000001', CAST(0x0000A695000172C2 AS DateTime), CAST(0 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), N'', N'', CAST(1 AS Numeric(18, 0)), N'fa fa-group', CAST(1 AS Numeric(3, 0)))
INSERT [dbo].[SEG_Opcion] ([CO_OPCION], [NO_OPCION], [RT_OPCION], [ST_REGISTRO], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI], [CO_OPCION_PADRE], [CO_APLICACION], [CO_NIVEL], [TI_OPEN], [IMG_OPCION], [DES_OPCION], [TI_PAR_RUTA], [AB_OPCION], [NU_ORDEN]) VALUES (CAST(2 AS Numeric(18, 0)), N'Planificación', N'', CAST(1 AS Numeric(1, 0)), N'00000000000001', CAST(0x0000A69500017606 AS DateTime), N'00000000000001', CAST(0x0000A69500017606 AS DateTime), CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), N'', N'', CAST(1 AS Numeric(18, 0)), N'fa fa-fw fa-envelope', CAST(1 AS Numeric(3, 0)))
INSERT [dbo].[SEG_Opcion] ([CO_OPCION], [NO_OPCION], [RT_OPCION], [ST_REGISTRO], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI], [CO_OPCION_PADRE], [CO_APLICACION], [CO_NIVEL], [TI_OPEN], [IMG_OPCION], [DES_OPCION], [TI_PAR_RUTA], [AB_OPCION], [NU_ORDEN]) VALUES (CAST(3 AS Numeric(18, 0)), N'Gestionar Plan de Compras', N'Planificacion/pgGestionPlanCompra.html', CAST(1 AS Numeric(1, 0)), N'00000000000001', CAST(0x0000A69500017898 AS DateTime), N'00000000000001', CAST(0x0000A69500017898 AS DateTime), CAST(2 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), N'', N'', CAST(1 AS Numeric(18, 0)), N'fa fa-bell-o', CAST(1 AS Numeric(3, 0)))
INSERT [dbo].[SEG_Opcion] ([CO_OPCION], [NO_OPCION], [RT_OPCION], [ST_REGISTRO], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI], [CO_OPCION_PADRE], [CO_APLICACION], [CO_NIVEL], [TI_OPEN], [IMG_OPCION], [DES_OPCION], [TI_PAR_RUTA], [AB_OPCION], [NU_ORDEN]) VALUES (CAST(4 AS Numeric(18, 0)), N'Ejecución', N'', CAST(1 AS Numeric(1, 0)), N'00000000000001', CAST(0x0000A69500018027 AS DateTime), N'00000000000001', CAST(0x0000A69500018027 AS DateTime), CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), N'', N'', CAST(1 AS Numeric(18, 0)), N'fa fa-fw fa-envelope', CAST(1 AS Numeric(3, 0)))
INSERT [dbo].[SEG_Opcion] ([CO_OPCION], [NO_OPCION], [RT_OPCION], [ST_REGISTRO], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI], [CO_OPCION_PADRE], [CO_APLICACION], [CO_NIVEL], [TI_OPEN], [IMG_OPCION], [DES_OPCION], [TI_PAR_RUTA], [AB_OPCION], [NU_ORDEN]) VALUES (CAST(5 AS Numeric(18, 0)), N'Gestionar Orden de Compra', N'Ejecucion/pgGestionOrdenCompra.html', CAST(1 AS Numeric(1, 0)), N'00000000000001', CAST(0x0000A6950001D21B AS DateTime), N'00000000000001', CAST(0x0000A6950001D21B AS DateTime), CAST(4 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), N'', N'', CAST(1 AS Numeric(18, 0)), N'fa fa-bell-o', CAST(1 AS Numeric(3, 0)))
INSERT [dbo].[SEG_Opcion] ([CO_OPCION], [NO_OPCION], [RT_OPCION], [ST_REGISTRO], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI], [CO_OPCION_PADRE], [CO_APLICACION], [CO_NIVEL], [TI_OPEN], [IMG_OPCION], [DES_OPCION], [TI_PAR_RUTA], [AB_OPCION], [NU_ORDEN]) VALUES (CAST(6 AS Numeric(18, 0)), N'Administración', NULL, CAST(1 AS Numeric(1, 0)), N'00000000000001', CAST(0x0000A69500000000 AS DateTime), N'00000000000001', CAST(0x0000A69500000000 AS DateTime), CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)), NULL, NULL, CAST(1 AS Numeric(18, 0)), N'fa fa-fw fa-envelope', CAST(1 AS Numeric(3, 0)))
INSERT [dbo].[SEG_Opcion] ([CO_OPCION], [NO_OPCION], [RT_OPCION], [ST_REGISTRO], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI], [CO_OPCION_PADRE], [CO_APLICACION], [CO_NIVEL], [TI_OPEN], [IMG_OPCION], [DES_OPCION], [TI_PAR_RUTA], [AB_OPCION], [NU_ORDEN]) VALUES (CAST(7 AS Numeric(18, 0)), N'Gestionar Solicitud de Recursos', N'Administracion/pgActualizarInformacionSolicitud.html', CAST(1 AS Numeric(1, 0)), N'00000000000001', CAST(0x0000A6FF00000000 AS DateTime), N'00000000000001', CAST(0x0000A6FF00000000 AS DateTime), CAST(6 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, NULL, CAST(1 AS Numeric(18, 0)), N'fa fa-bell-o', CAST(1 AS Numeric(3, 0)))
INSERT [dbo].[SEG_Opcion] ([CO_OPCION], [NO_OPCION], [RT_OPCION], [ST_REGISTRO], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI], [CO_OPCION_PADRE], [CO_APLICACION], [CO_NIVEL], [TI_OPEN], [IMG_OPCION], [DES_OPCION], [TI_PAR_RUTA], [AB_OPCION], [NU_ORDEN]) VALUES (CAST(8 AS Numeric(18, 0)), N'Actualizar Información del Proveedor', N'Administracion/pgGestionProveedor.html', CAST(1 AS Numeric(1, 0)), N'00000000000001', CAST(0x0000A6FF00000000 AS DateTime), N'00000000000001', CAST(0x0000A6FF00000000 AS DateTime), CAST(6 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)), NULL, NULL, CAST(1 AS Numeric(18, 0)), N'fa fa-bell-o', CAST(1 AS Numeric(3, 0)))
INSERT [dbo].[SEG_Rol] ([CO_ROL], [DE_ROL], [ST_REGISTRO], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI], [CO_APLICACION]) VALUES (CAST(1 AS Numeric(18, 0)), N'Analista de Compras', CAST(1 AS Numeric(1, 0)), N'00000000000001', CAST(0x0000A694012A6674 AS DateTime), N'00000000000001', CAST(0x0000A694012A6674 AS DateTime), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[SEG_Rol] ([CO_ROL], [DE_ROL], [ST_REGISTRO], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI], [CO_APLICACION]) VALUES (CAST(2 AS Numeric(18, 0)), N'Registrador de Clientes', CAST(1 AS Numeric(1, 0)), N'00000000000001', CAST(0x0000A7030106A252 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[SEG_RolxOpcion] ([CO_ROL], [CO_OPCION]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[SEG_RolxOpcion] ([CO_ROL], [CO_OPCION]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(2 AS Numeric(18, 0)))
INSERT [dbo].[SEG_RolxOpcion] ([CO_ROL], [CO_OPCION]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(3 AS Numeric(18, 0)))
INSERT [dbo].[SEG_RolxOpcion] ([CO_ROL], [CO_OPCION]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(4 AS Numeric(18, 0)))
INSERT [dbo].[SEG_RolxOpcion] ([CO_ROL], [CO_OPCION]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(5 AS Numeric(18, 0)))
INSERT [dbo].[SEG_RolxOpcion] ([CO_ROL], [CO_OPCION]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(6 AS Numeric(18, 0)))
INSERT [dbo].[SEG_RolxOpcion] ([CO_ROL], [CO_OPCION]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(7 AS Numeric(18, 0)))
INSERT [dbo].[SEG_RolxOpcion] ([CO_ROL], [CO_OPCION]) VALUES (CAST(1 AS Numeric(18, 0)), CAST(8 AS Numeric(18, 0)))
INSERT [dbo].[SEG_Usuario] ([CO_USUA], [AL_USUA], [NO_USUA], [AP_USUA], [AM_USUA], [PW_USUA], [DNI_USUA], [NO_EMAIL], [NU_TELEFONO], [DE_DIRECCION], [ST_REGISTRO], [CO_AREA], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI]) VALUES (N'00000000000001', N'agrey', N'Allan Davis', N'Grey', N'Ferrari', N'bgyz0448', N'43695870', N'allan.grey@talma.com.pe', NULL, N'Av. Camino Real 371 - Santiago de Surco', CAST(1 AS Numeric(1, 0)), CAST(1 AS Numeric(4, 0)), N'00000000000001', CAST(0x0000A694011826C0 AS DateTime), N'00000000000001', CAST(0x0000A694011826C0 AS DateTime))
INSERT [dbo].[SEG_Usuario] ([CO_USUA], [AL_USUA], [NO_USUA], [AP_USUA], [AM_USUA], [PW_USUA], [DNI_USUA], [NO_EMAIL], [NU_TELEFONO], [DE_DIRECCION], [ST_REGISTRO], [CO_AREA], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI]) VALUES (N'00000000000002', N'ymateo', N'Yuri', N'Mateo', N'', N'111222333', N'22222222', N'', N'', N'', CAST(1 AS Numeric(1, 0)), CAST(1 AS Numeric(4, 0)), N'00000000000002', CAST(0x0000A69500F1B0B1 AS DateTime), N'00000000000002', CAST(0x0000A69500F1B0B1 AS DateTime))
INSERT [dbo].[SEG_Usuario] ([CO_USUA], [AL_USUA], [NO_USUA], [AP_USUA], [AM_USUA], [PW_USUA], [DNI_USUA], [NO_EMAIL], [NU_TELEFONO], [DE_DIRECCION], [ST_REGISTRO], [CO_AREA], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI]) VALUES (N'00000000000003', N'ipurizaga', N'Israel', N'Purizaga', N'', N'111222333', N'22222222', N'', N'', N'', CAST(1 AS Numeric(1, 0)), CAST(1 AS Numeric(4, 0)), N'00000000000002', CAST(0x0000A69500F1D787 AS DateTime), N'00000000000002', CAST(0x0000A69500F1D787 AS DateTime))
INSERT [dbo].[SEG_Usuario] ([CO_USUA], [AL_USUA], [NO_USUA], [AP_USUA], [AM_USUA], [PW_USUA], [DNI_USUA], [NO_EMAIL], [NU_TELEFONO], [DE_DIRECCION], [ST_REGISTRO], [CO_AREA], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI]) VALUES (N'00000000000004', N'cbustamante', N'Christian', N'Bustamante', N'Salcedo', N'cbustamante', N'44587412', N'cbustamante@petcenter.com', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A7030107AB6B AS DateTime), NULL, NULL)
INSERT [dbo].[SEG_Usuario] ([CO_USUA], [AL_USUA], [NO_USUA], [AP_USUA], [AM_USUA], [PW_USUA], [DNI_USUA], [NO_EMAIL], [NU_TELEFONO], [DE_DIRECCION], [ST_REGISTRO], [CO_AREA], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI]) VALUES (N'00000000000006', N'glujan', N'Gary', N'Luján', NULL, N'glujan', N'44124587', N'glujan@petcenter.com', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A70301082F9B AS DateTime), NULL, NULL)
INSERT [dbo].[SEG_Usuario] ([CO_USUA], [AL_USUA], [NO_USUA], [AP_USUA], [AM_USUA], [PW_USUA], [DNI_USUA], [NO_EMAIL], [NU_TELEFONO], [DE_DIRECCION], [ST_REGISTRO], [CO_AREA], [CO_USUA_CREA], [FE_USUA_CREA], [CO_USUA_MODI], [FE_USUA_MODI]) VALUES (N'00000000000005', N'magurto', N'María', N'Agurto', N'Gutierrez', N'magurto', N'45795142', N'magurto@petcenter.com', NULL, NULL, NULL, NULL, NULL, CAST(0x0000A70301083281 AS DateTime), NULL, NULL)
INSERT [dbo].[SEG_UsuarioxRol] ([CO_USUA], [CO_ROL]) VALUES (N'00000000000001', CAST(1 AS Numeric(18, 0)))
INSERT [dbo].[SEG_UsuarioxRol] ([CO_USUA], [CO_ROL]) VALUES (N'00000000000004', CAST(2 AS Numeric(18, 0)))
INSERT [dbo].[SEG_UsuarioxRol] ([CO_USUA], [CO_ROL]) VALUES (N'00000000000006', CAST(2 AS Numeric(18, 0)))
INSERT [dbo].[SEG_UsuarioxRol] ([CO_USUA], [CO_ROL]) VALUES (N'00000000000005', CAST(2 AS Numeric(18, 0)))
ALTER TABLE [dbo].[ACI_Chip]  WITH CHECK ADD  CONSTRAINT [FK_ACI_Chip_GCP_Paciente] FOREIGN KEY([id_paciente])
REFERENCES [dbo].[GCP_Paciente] ([Id_Mascota])
GO
ALTER TABLE [dbo].[ACI_Chip] CHECK CONSTRAINT [FK_ACI_Chip_GCP_Paciente]
GO
ALTER TABLE [dbo].[ACI_HojaEscaneo]  WITH CHECK ADD  CONSTRAINT [FK_HojaEscaneo_Empleados] FOREIGN KEY([Id_Empleado])
REFERENCES [dbo].[GHA_Empleado] ([id_Empleado])
GO
ALTER TABLE [dbo].[ACI_HojaEscaneo] CHECK CONSTRAINT [FK_HojaEscaneo_Empleados]
GO
ALTER TABLE [dbo].[ACI_HojaEscaneo]  WITH CHECK ADD  CONSTRAINT [FK_HojaEscaneo_OrdenAtencion] FOREIGN KEY([idOrdenAtencion])
REFERENCES [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion])
GO
ALTER TABLE [dbo].[ACI_HojaEscaneo] CHECK CONSTRAINT [FK_HojaEscaneo_OrdenAtencion]
GO
ALTER TABLE [dbo].[ACI_SolicitudAtencion]  WITH CHECK ADD  CONSTRAINT [FK_ACI_SolicitudAtencion_GCP_Paciente] FOREIGN KEY([id_Mascota])
REFERENCES [dbo].[GCP_Paciente] ([Id_Mascota])
GO
ALTER TABLE [dbo].[ACI_SolicitudAtencion] CHECK CONSTRAINT [FK_ACI_SolicitudAtencion_GCP_Paciente]
GO
ALTER TABLE [dbo].[ACI_TarjetaIdentificacion]  WITH CHECK ADD  CONSTRAINT [FK_ACI_TarjetaIdentificacion_GCP_Paciente] FOREIGN KEY([id_Mascota])
REFERENCES [dbo].[GCP_Paciente] ([Id_Mascota])
GO
ALTER TABLE [dbo].[ACI_TarjetaIdentificacion] CHECK CONSTRAINT [FK_ACI_TarjetaIdentificacion_GCP_Paciente]
GO
ALTER TABLE [dbo].[GCP_Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_id_Distrito] FOREIGN KEY([id_Distrito])
REFERENCES [dbo].[GCP_Distrito] ([id_Distrito])
GO
ALTER TABLE [dbo].[GCP_Cliente] CHECK CONSTRAINT [FK_Cliente_id_Distrito]
GO
ALTER TABLE [dbo].[GCP_Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_id_TipoCliente] FOREIGN KEY([Tipo_Cliente])
REFERENCES [dbo].[GCP_TipoCliente] ([id_TipoCliente])
GO
ALTER TABLE [dbo].[GCP_Cliente] CHECK CONSTRAINT [FK_Cliente_id_TipoCliente]
GO
ALTER TABLE [dbo].[GCP_Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_id_TipoDocumento] FOREIGN KEY([TipoDocumento_Identidad])
REFERENCES [dbo].[GCP_TipoDocumento] ([id_TipoDocumento])
GO
ALTER TABLE [dbo].[GCP_Cliente] CHECK CONSTRAINT [FK_Cliente_id_TipoDocumento]
GO
ALTER TABLE [dbo].[GCP_ClientePacienteHistorico]  WITH CHECK ADD  CONSTRAINT [FK_ClientePacienteHistorico_id_Paciente] FOREIGN KEY([id_Paciente])
REFERENCES [dbo].[GCP_Paciente] ([Id_Mascota])
GO
ALTER TABLE [dbo].[GCP_ClientePacienteHistorico] CHECK CONSTRAINT [FK_ClientePacienteHistorico_id_Paciente]
GO
ALTER TABLE [dbo].[GCP_Notificacion]  WITH CHECK ADD  CONSTRAINT [FK_idOrdenAtencion] FOREIGN KEY([id_OrdenAtencion])
REFERENCES [dbo].[GCP_OrdenAtencion] ([idOrdenAtencion])
GO
ALTER TABLE [dbo].[GCP_Notificacion] CHECK CONSTRAINT [FK_idOrdenAtencion]
GO
ALTER TABLE [dbo].[GCP_OrdenAtencion]  WITH CHECK ADD  CONSTRAINT [FK_GCP_OrdenAtencion_GCP_Paciente] FOREIGN KEY([id_Mascota])
REFERENCES [dbo].[GCP_Paciente] ([Id_Mascota])
GO
ALTER TABLE [dbo].[GCP_OrdenAtencion] CHECK CONSTRAINT [FK_GCP_OrdenAtencion_GCP_Paciente]
GO
ALTER TABLE [dbo].[GCP_OrdenAtencion]  WITH CHECK ADD  CONSTRAINT [FK_OrdenAtencion_estado] FOREIGN KEY([estado])
REFERENCES [dbo].[GCP_EstadoOrden] ([codigo])
GO
ALTER TABLE [dbo].[GCP_OrdenAtencion] CHECK CONSTRAINT [FK_OrdenAtencion_estado]
GO
ALTER TABLE [dbo].[GCP_OrdenAtencion]  WITH CHECK ADD  CONSTRAINT [FK_OrdenAtencion_id_MotivoRechazo] FOREIGN KEY([id_MotivoRechazo])
REFERENCES [dbo].[ACI_MotivoRechazo] ([id_MotivoRechazo])
GO
ALTER TABLE [dbo].[GCP_OrdenAtencion] CHECK CONSTRAINT [FK_OrdenAtencion_id_MotivoRechazo]
GO
ALTER TABLE [dbo].[GCP_OrdenAtencion]  WITH CHECK ADD  CONSTRAINT [FK_OrdenAtencion_id_Servicio] FOREIGN KEY([id_Servicio])
REFERENCES [dbo].[GCP_Servicio] ([id_Servicio])
GO
ALTER TABLE [dbo].[GCP_OrdenAtencion] CHECK CONSTRAINT [FK_OrdenAtencion_id_Servicio]
GO
ALTER TABLE [dbo].[GCP_OrdenAtencion]  WITH CHECK ADD  CONSTRAINT [FK_OrdenAtencion_id_Turno] FOREIGN KEY([id_Turno])
REFERENCES [dbo].[GCP_Turno] ([id_Turno])
GO
ALTER TABLE [dbo].[GCP_OrdenAtencion] CHECK CONSTRAINT [FK_OrdenAtencion_id_Turno]
GO
ALTER TABLE [dbo].[GCP_Paciente]  WITH CHECK ADD  CONSTRAINT [FK_Mascota_Cliente] FOREIGN KEY([Id_Cliente])
REFERENCES [dbo].[GCP_Cliente] ([idCliente])
GO
ALTER TABLE [dbo].[GCP_Paciente] CHECK CONSTRAINT [FK_Mascota_Cliente]
GO
ALTER TABLE [dbo].[GCP_Paciente]  WITH CHECK ADD  CONSTRAINT [FK_Paciente_id_Raza] FOREIGN KEY([id_Raza])
REFERENCES [dbo].[GCP_Raza] ([id_Raza])
GO
ALTER TABLE [dbo].[GCP_Paciente] CHECK CONSTRAINT [FK_Paciente_id_Raza]
GO
ALTER TABLE [dbo].[GCP_Raza]  WITH CHECK ADD  CONSTRAINT [FK_Raza_id_Especie] FOREIGN KEY([id_Especie])
REFERENCES [dbo].[GCP_Especie] ([id_Especie])
GO
ALTER TABLE [dbo].[GCP_Raza] CHECK CONSTRAINT [FK_Raza_id_Especie]
GO
ALTER TABLE [dbo].[GCP_Sede]  WITH CHECK ADD  CONSTRAINT [FK_Sede_id_Distrito] FOREIGN KEY([id_Distrito])
REFERENCES [dbo].[GCP_Distrito] ([id_Distrito])
GO
ALTER TABLE [dbo].[GCP_Sede] CHECK CONSTRAINT [FK_Sede_id_Distrito]
GO
ALTER TABLE [dbo].[GCP_SedeServicio]  WITH CHECK ADD  CONSTRAINT [FK_SedeServicio_id_Sede] FOREIGN KEY([id_Sede])
REFERENCES [dbo].[GCP_Sede] ([id_Sede])
GO
ALTER TABLE [dbo].[GCP_SedeServicio] CHECK CONSTRAINT [FK_SedeServicio_id_Sede]
GO
ALTER TABLE [dbo].[GCP_SedeServicio]  WITH CHECK ADD  CONSTRAINT [FK_SedeServicio_id_Servicio] FOREIGN KEY([id_Servicio])
REFERENCES [dbo].[GCP_Servicio] ([id_Servicio])
GO
ALTER TABLE [dbo].[GCP_SedeServicio] CHECK CONSTRAINT [FK_SedeServicio_id_Servicio]
GO
ALTER TABLE [dbo].[GHA_Asignacion_turnos]  WITH CHECK ADD  CONSTRAINT [FK_Asignacion_turnos_Empleados] FOREIGN KEY([Id_Empleado])
REFERENCES [dbo].[GHA_Empleado] ([id_Empleado])
GO
ALTER TABLE [dbo].[GHA_Asignacion_turnos] CHECK CONSTRAINT [FK_Asignacion_turnos_Empleados]
GO
ALTER TABLE [dbo].[GHA_Asignacion_turnos]  WITH NOCHECK ADD  CONSTRAINT [FK_Asignacion_turnos_Expediente] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[GHA_ServicioHospedaje] ([Id_Servicio])
GO
ALTER TABLE [dbo].[GHA_Asignacion_turnos] NOCHECK CONSTRAINT [FK_Asignacion_turnos_Expediente]
GO
ALTER TABLE [dbo].[GHA_Canil]  WITH CHECK ADD  CONSTRAINT [FK_Canil_Especie] FOREIGN KEY([TipoEspecie])
REFERENCES [dbo].[GCP_Especie] ([id_Especie])
GO
ALTER TABLE [dbo].[GHA_Canil] CHECK CONSTRAINT [FK_Canil_Especie]
GO
ALTER TABLE [dbo].[GHA_Empleado]  WITH CHECK ADD  CONSTRAINT [FK_GHA_Empleado_GPC_Area] FOREIGN KEY([idArea])
REFERENCES [dbo].[GPC_Area] ([idArea])
GO
ALTER TABLE [dbo].[GHA_Empleado] CHECK CONSTRAINT [FK_GHA_Empleado_GPC_Area]
GO
ALTER TABLE [dbo].[GHA_Expediente_Hospedaje]  WITH CHECK ADD  CONSTRAINT [FK_Mascota_Expediente] FOREIGN KEY([Id_Mascota])
REFERENCES [dbo].[GCP_Paciente] ([Id_Mascota])
GO
ALTER TABLE [dbo].[GHA_Expediente_Hospedaje] CHECK CONSTRAINT [FK_Mascota_Expediente]
GO
ALTER TABLE [dbo].[GHA_Plan_Alimenticio_Cab]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Alimenticio_Cab_Estado] FOREIGN KEY([Estado])
REFERENCES [dbo].[GHA_Estado] ([Id_Estado])
GO
ALTER TABLE [dbo].[GHA_Plan_Alimenticio_Cab] CHECK CONSTRAINT [FK_Plan_Alimenticio_Cab_Estado]
GO
ALTER TABLE [dbo].[GHA_Plan_Alimenticio_Cab]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Alimenticio_Cab_Expediente] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[GHA_ServicioHospedaje] ([Id_Servicio])
GO
ALTER TABLE [dbo].[GHA_Plan_Alimenticio_Cab] CHECK CONSTRAINT [FK_Plan_Alimenticio_Cab_Expediente]
GO
ALTER TABLE [dbo].[GHA_Plan_Alimenticio_Cab]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Alimenticio_Cab_Objetivo_Plan] FOREIGN KEY([Id_Objetivo])
REFERENCES [dbo].[GHA_Objetivo_Plan] ([Id_Objetivo])
GO
ALTER TABLE [dbo].[GHA_Plan_Alimenticio_Cab] CHECK CONSTRAINT [FK_Plan_Alimenticio_Cab_Objetivo_Plan]
GO
ALTER TABLE [dbo].[GHA_Plan_Alimenticio_Det]  WITH CHECK ADD FOREIGN KEY([Id_Plan])
REFERENCES [dbo].[GHA_Plan_Alimenticio_Cab] ([Id_Plan])
GO
ALTER TABLE [dbo].[GHA_Plan_Alimenticio_Det]  WITH NOCHECK ADD  CONSTRAINT [FK__Plan_Alim__Id_Ti__3DE82FB7] FOREIGN KEY([Id_Tipo_Alimento])
REFERENCES [dbo].[GHA_Alimento] ([Id_Tipo_Alimento])
GO
ALTER TABLE [dbo].[GHA_Plan_Alimenticio_Det] NOCHECK CONSTRAINT [FK__Plan_Alim__Id_Ti__3DE82FB7]
GO
ALTER TABLE [dbo].[GHA_Plan_Medicamentos]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Medicamentos_Expediente] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[GHA_ServicioHospedaje] ([Id_Servicio])
GO
ALTER TABLE [dbo].[GHA_Plan_Medicamentos] CHECK CONSTRAINT [FK_Plan_Medicamentos_Expediente]
GO
ALTER TABLE [dbo].[GHA_Plan_Medicamentos]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Medicamentos_Tipo_Medicamento] FOREIGN KEY([id_TipoMedicamento])
REFERENCES [dbo].[GHA_Tipo_Medicamento] ([id_TipoMedicamento])
GO
ALTER TABLE [dbo].[GHA_Plan_Medicamentos] CHECK CONSTRAINT [FK_Plan_Medicamentos_Tipo_Medicamento]
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Rutina_Expediente] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[GHA_ServicioHospedaje] ([Id_Servicio])
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina] CHECK CONSTRAINT [FK_Plan_Rutina_Expediente]
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Rutina_Tipo_Rutina] FOREIGN KEY([id_TipoRutina])
REFERENCES [dbo].[GHA_Tipo_Rutina] ([id_TipoRutina])
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina] CHECK CONSTRAINT [FK_Plan_Rutina_Tipo_Rutina]
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina_Cab]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Rutina_Cab_Estado] FOREIGN KEY([Estado])
REFERENCES [dbo].[GHA_Estado] ([Id_Estado])
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina_Cab] CHECK CONSTRAINT [FK_Plan_Rutina_Cab_Estado]
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina_Cab]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Rutina_Cab_Expediente] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[GHA_ServicioHospedaje] ([Id_Servicio])
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina_Cab] CHECK CONSTRAINT [FK_Plan_Rutina_Cab_Expediente]
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina_Det]  WITH CHECK ADD FOREIGN KEY([Id_Plan])
REFERENCES [dbo].[GHA_Plan_Rutina_Cab] ([Id_Plan])
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina_Det]  WITH NOCHECK ADD  CONSTRAINT [FK__Plan_rutina__Id_Ti__3DE82FB7] FOREIGN KEY([Id_Tipo_Rutina])
REFERENCES [dbo].[GHA_Alimento] ([Id_Tipo_Alimento])
GO
ALTER TABLE [dbo].[GHA_Plan_Rutina_Det] NOCHECK CONSTRAINT [FK__Plan_rutina__Id_Ti__3DE82FB7]
GO
ALTER TABLE [dbo].[GHA_Plan_Suplementos]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Suplementos_Expediente] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[GHA_ServicioHospedaje] ([Id_Servicio])
GO
ALTER TABLE [dbo].[GHA_Plan_Suplementos] CHECK CONSTRAINT [FK_Plan_Suplementos_Expediente]
GO
ALTER TABLE [dbo].[GHA_Plan_Suplementos]  WITH CHECK ADD  CONSTRAINT [FK_Plan_Suplementos_Tipo_Suplemento] FOREIGN KEY([id_TipoSuplemento])
REFERENCES [dbo].[GHA_Tipo_Suplemento] ([id_TipoSuplemento])
GO
ALTER TABLE [dbo].[GHA_Plan_Suplementos] CHECK CONSTRAINT [FK_Plan_Suplementos_Tipo_Suplemento]
GO
ALTER TABLE [dbo].[GHA_Reserva_Cita]  WITH CHECK ADD  CONSTRAINT [FK_Reserva_Cita_Mascota] FOREIGN KEY([Id_Mascota])
REFERENCES [dbo].[GCP_Paciente] ([Id_Mascota])
GO
ALTER TABLE [dbo].[GHA_Reserva_Cita] CHECK CONSTRAINT [FK_Reserva_Cita_Mascota]
GO
ALTER TABLE [dbo].[GHA_Revision_Medica]  WITH CHECK ADD  CONSTRAINT [FK_Revision_Medica_Expediente] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[GHA_ServicioHospedaje] ([Id_Servicio])
GO
ALTER TABLE [dbo].[GHA_Revision_Medica] CHECK CONSTRAINT [FK_Revision_Medica_Expediente]
GO
ALTER TABLE [dbo].[GHA_ServicioHospedaje]  WITH CHECK ADD  CONSTRAINT [FK_Expediente_Canil] FOREIGN KEY([Id_Canil])
REFERENCES [dbo].[GHA_Canil] ([Id_Canil])
GO
ALTER TABLE [dbo].[GHA_ServicioHospedaje] CHECK CONSTRAINT [FK_Expediente_Canil]
GO
ALTER TABLE [dbo].[GHA_ServicioHospedaje]  WITH CHECK ADD  CONSTRAINT [FK_Expediente_Reserva_Cita] FOREIGN KEY([Id_Reserva])
REFERENCES [dbo].[GHA_Reserva_Cita] ([Id_Reserva])
GO
ALTER TABLE [dbo].[GHA_ServicioHospedaje] CHECK CONSTRAINT [FK_Expediente_Reserva_Cita]
GO
ALTER TABLE [dbo].[GHA_Ticket_Traslado]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Traslado_Expediente] FOREIGN KEY([Id_Servicio])
REFERENCES [dbo].[GHA_ServicioHospedaje] ([Id_Servicio])
GO
ALTER TABLE [dbo].[GHA_Ticket_Traslado] CHECK CONSTRAINT [FK_Ticket_Traslado_Expediente]
GO
ALTER TABLE [dbo].[GHA_Ticket_Traslado]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Traslado_Tipo_Traslado] FOREIGN KEY([id_Tipotraslado])
REFERENCES [dbo].[GHA_Tipo_Traslado] ([id_Tipotraslado])
GO
ALTER TABLE [dbo].[GHA_Ticket_Traslado] CHECK CONSTRAINT [FK_Ticket_Traslado_Tipo_Traslado]
GO
ALTER TABLE [dbo].[GPC_CriterioEvaluacion]  WITH CHECK ADD  CONSTRAINT [FK_GPC_CriterioEvaluacion_GPC_EvaluacionProveedor] FOREIGN KEY([idEvaluacionProveedor])
REFERENCES [dbo].[GPC_EvaluacionProveedor] ([idEvaluacionProveedor])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GPC_CriterioEvaluacion] CHECK CONSTRAINT [FK_GPC_CriterioEvaluacion_GPC_EvaluacionProveedor]
GO
ALTER TABLE [dbo].[GPC_EvaluacionProveedor]  WITH CHECK ADD  CONSTRAINT [FK_GPC_EvaluacionProveedor_GPC_Proveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[GPC_Proveedor] ([idProveedor])
GO
ALTER TABLE [dbo].[GPC_EvaluacionProveedor] CHECK CONSTRAINT [FK_GPC_EvaluacionProveedor_GPC_Proveedor]
GO
ALTER TABLE [dbo].[GPC_IncidenciaProveedor]  WITH CHECK ADD  CONSTRAINT [FK_GPC_IncidenciaProveedor_GPC_OrdenCompra] FOREIGN KEY([idOrdenCompra])
REFERENCES [dbo].[GPC_OrdenCompra] ([idOrdenCompra])
GO
ALTER TABLE [dbo].[GPC_IncidenciaProveedor] CHECK CONSTRAINT [FK_GPC_IncidenciaProveedor_GPC_OrdenCompra]
GO
ALTER TABLE [dbo].[GPC_ItemOrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_GPC_ItemOrdenCompra_GPC_OrdenCompra] FOREIGN KEY([idOrdenCompra])
REFERENCES [dbo].[GPC_OrdenCompra] ([idOrdenCompra])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GPC_ItemOrdenCompra] CHECK CONSTRAINT [FK_GPC_ItemOrdenCompra_GPC_OrdenCompra]
GO
ALTER TABLE [dbo].[GPC_ItemOrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_GPC_ItemOrdenCompra_GPC_RecursoProveedor] FOREIGN KEY([idRecursoProveedor])
REFERENCES [dbo].[GPC_RecursoProveedor] ([idRecursoProveedor])
GO
ALTER TABLE [dbo].[GPC_ItemOrdenCompra] CHECK CONSTRAINT [FK_GPC_ItemOrdenCompra_GPC_RecursoProveedor]
GO
ALTER TABLE [dbo].[GPC_ItemPlanCompras]  WITH CHECK ADD  CONSTRAINT [FK_GPC_ItemPlanCompras_GPC_PlanCompras] FOREIGN KEY([idPlanCompras])
REFERENCES [dbo].[GPC_PlanCompras] ([idPlanCompras])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GPC_ItemPlanCompras] CHECK CONSTRAINT [FK_GPC_ItemPlanCompras_GPC_PlanCompras]
GO
ALTER TABLE [dbo].[GPC_ItemPlanCompras]  WITH CHECK ADD  CONSTRAINT [FK_GPC_ItemPlanCompras_GPC_RecursoProveedor] FOREIGN KEY([idRecursoProveedor])
REFERENCES [dbo].[GPC_RecursoProveedor] ([idRecursoProveedor])
GO
ALTER TABLE [dbo].[GPC_ItemPlanCompras] CHECK CONSTRAINT [FK_GPC_ItemPlanCompras_GPC_RecursoProveedor]
GO
ALTER TABLE [dbo].[GPC_ItemSolicitudRecursos]  WITH CHECK ADD  CONSTRAINT [FK_GPC_ItemSolicitudRecursos_GPC_PresentacionRecurso] FOREIGN KEY([idPresentacionRecurso])
REFERENCES [dbo].[GPC_PresentacionRecurso] ([idPresentacionRecurso])
GO
ALTER TABLE [dbo].[GPC_ItemSolicitudRecursos] CHECK CONSTRAINT [FK_GPC_ItemSolicitudRecursos_GPC_PresentacionRecurso]
GO
ALTER TABLE [dbo].[GPC_ItemSolicitudRecursos]  WITH CHECK ADD  CONSTRAINT [FK_GPC_ItemSolicitudRecursos_GPC_SolicitudRecursos] FOREIGN KEY([IdSolicitudRecursos])
REFERENCES [dbo].[GPC_SolicitudRecursos] ([idSolicitudRecursos])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GPC_ItemSolicitudRecursos] CHECK CONSTRAINT [FK_GPC_ItemSolicitudRecursos_GPC_SolicitudRecursos]
GO
ALTER TABLE [dbo].[GPC_OrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_GPC_OrdenCompra_GHA_Empleado] FOREIGN KEY([idEmpleado])
REFERENCES [dbo].[GHA_Empleado] ([id_Empleado])
GO
ALTER TABLE [dbo].[GPC_OrdenCompra] CHECK CONSTRAINT [FK_GPC_OrdenCompra_GHA_Empleado]
GO
ALTER TABLE [dbo].[GPC_OrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_GPC_OrdenCompra_GPC_PlanCompras] FOREIGN KEY([idPlanCompras])
REFERENCES [dbo].[GPC_PlanCompras] ([idPlanCompras])
GO
ALTER TABLE [dbo].[GPC_OrdenCompra] CHECK CONSTRAINT [FK_GPC_OrdenCompra_GPC_PlanCompras]
GO
ALTER TABLE [dbo].[GPC_OrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_GPC_OrdenCompra_GPC_Proveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[GPC_Proveedor] ([idProveedor])
GO
ALTER TABLE [dbo].[GPC_OrdenCompra] CHECK CONSTRAINT [FK_GPC_OrdenCompra_GPC_Proveedor]
GO
ALTER TABLE [dbo].[GPC_OrdenCompra]  WITH CHECK ADD  CONSTRAINT [FK_GPC_OrdenCompra_GPC_SolicitudRecursos] FOREIGN KEY([idSolicitudRecursos])
REFERENCES [dbo].[GPC_SolicitudRecursos] ([idSolicitudRecursos])
GO
ALTER TABLE [dbo].[GPC_OrdenCompra] CHECK CONSTRAINT [FK_GPC_OrdenCompra_GPC_SolicitudRecursos]
GO
ALTER TABLE [dbo].[GPC_PlanCompras]  WITH CHECK ADD  CONSTRAINT [FK_GPC_PlanCompras_GHA_Empleado] FOREIGN KEY([idEmpleado])
REFERENCES [dbo].[GHA_Empleado] ([id_Empleado])
GO
ALTER TABLE [dbo].[GPC_PlanCompras] CHECK CONSTRAINT [FK_GPC_PlanCompras_GHA_Empleado]
GO
ALTER TABLE [dbo].[GPC_PresentacionRecurso]  WITH CHECK ADD  CONSTRAINT [FK_GPC_PresentacionRecurso_GPC_Recurso] FOREIGN KEY([idRecurso])
REFERENCES [dbo].[GPC_Recurso] ([idRecurso])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GPC_PresentacionRecurso] CHECK CONSTRAINT [FK_GPC_PresentacionRecurso_GPC_Recurso]
GO
ALTER TABLE [dbo].[GPC_Presupuesto]  WITH CHECK ADD  CONSTRAINT [FK_GPC_Presupuesto_GPC_Area] FOREIGN KEY([idArea])
REFERENCES [dbo].[GPC_Area] ([idArea])
GO
ALTER TABLE [dbo].[GPC_Presupuesto] CHECK CONSTRAINT [FK_GPC_Presupuesto_GPC_Area]
GO
ALTER TABLE [dbo].[GPC_RecursoProveedor]  WITH CHECK ADD  CONSTRAINT [FK_GPC_RecursoProveedor_GPC_PresentacionRecurso] FOREIGN KEY([idPresentacionRecurso])
REFERENCES [dbo].[GPC_PresentacionRecurso] ([idPresentacionRecurso])
GO
ALTER TABLE [dbo].[GPC_RecursoProveedor] CHECK CONSTRAINT [FK_GPC_RecursoProveedor_GPC_PresentacionRecurso]
GO
ALTER TABLE [dbo].[GPC_RecursoProveedor]  WITH CHECK ADD  CONSTRAINT [FK_GPC_RecursoProveedor_GPC_Proveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[GPC_Proveedor] ([idProveedor])
GO
ALTER TABLE [dbo].[GPC_RecursoProveedor] CHECK CONSTRAINT [FK_GPC_RecursoProveedor_GPC_Proveedor]
GO
ALTER TABLE [dbo].[GPC_SolicitudRecursos]  WITH CHECK ADD  CONSTRAINT [FK_GPC_SolicitudRecursos_GHA_Empleado] FOREIGN KEY([idEmpleado])
REFERENCES [dbo].[GHA_Empleado] ([id_Empleado])
GO
ALTER TABLE [dbo].[GPC_SolicitudRecursos] CHECK CONSTRAINT [FK_GPC_SolicitudRecursos_GHA_Empleado]
GO
ALTER TABLE [dbo].[GPC_SolicitudRecursos]  WITH CHECK ADD  CONSTRAINT [FK_GPC_SolicitudRecursos_GPC_Area] FOREIGN KEY([idArea])
REFERENCES [dbo].[GPC_Area] ([idArea])
GO
ALTER TABLE [dbo].[GPC_SolicitudRecursos] CHECK CONSTRAINT [FK_GPC_SolicitudRecursos_GPC_Area]
GO
USE [master]
GO
ALTER DATABASE [BDPetCenter4] SET  READ_WRITE 
GO
