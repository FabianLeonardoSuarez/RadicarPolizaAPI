SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fabian Suarez
-- Create date: 04/05/2022
-- Description:	Realiza la busqueda de polizas por un valor string que puede ser placa o IdPoliza
-- =============================================
CREATE PROCEDURE SP_GetPolizaByPlacaOrIdPoliza
	@SearchKey varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT Polizas.* FROM Polizas
	INNER JOIN Automotor ON Automotor.IdAutomotor = Polizas.AutomotorIdAutomotor
	WHERE Automotor.PlacaAutomotor LIKE '%' + @SearchKey + '%' 
	OR Polizas.IdPoliza LIKE '%' + @SearchKey + '%';
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fabian Suarez
-- Create date: 04/05/2022
-- Description:	Crea una poliza a partir de los datos ingresados
-- =============================================
CREATE PROCEDURE SP_CreatePoliza 
	@IdPoliza uniqueidentifier,
	@IdCliente uniqueidentifier,
	@NombreCliente nvarchar(max),
	@IdentificacionCliente nvarchar(max),
	@TipoIdentificacionCliente int,
	@FechaNacimientoCliente datetime2(7),
	@FechaPoliza datetime2(7),
	@MaximoValorCobertura decimal(18,2),
	@PlanPoliza nvarchar(max),
	@CiudadResidenciaCliente nvarchar(max),
	@DireccionResidenciaCliente nvarchar(max),
	@IdAutomotor uniqueidentifier,
	@PlacaAutomotor nvarchar(6),
	@ModeloAutomotor nvarchar(max),
	@InspeccionAutomotor bit
AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO [dbo].[Cliente]
           ([IdCliente]
           ,[NombreCliente]
           ,[IdentificacionCliente]
           ,[TipoIdentificacionCliente]
           ,[FechaNacimientoCliente]
           ,[CiudadResidenciaCliente]
           ,[DireccionResidenciaCliente])
     VALUES
           (@IdCliente
           ,@NombreCliente
           ,@IdentificacionCliente
           ,@TipoIdentificacionCliente
           ,@FechaNacimientoCliente
           ,@CiudadResidenciaCliente
           ,@DireccionResidenciaCliente)

INSERT INTO [dbo].[Automotor]
           ([IdAutomotor]
           ,[PlacaAutomotor]
           ,[ModeloAutomotor]
           ,[InspeccionAutomotor])
     VALUES
           (@IdAutomotor
           ,@PlacaAutomotor
           ,@ModeloAutomotor
           ,@InspeccionAutomotor)

INSERT INTO [dbo].[Polizas]
           ([IdPoliza]
           ,[PlanPoliza]
           ,[FechaPoliza]
           ,[ClienteIdCliente]
           ,[AutomotorIdAutomotor]
           ,[MaximoValorCobertura])
     VALUES
           (@IdPoliza
           ,@PlanPoliza
           ,@FechaPoliza
           ,@IdCliente
           ,@IdAutomotor
           ,@MaximoValorCobertura)

END


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fabian Suarez
-- Create date: 04/05/2022
-- Description:	Crea una cobertura para una poliza dada
-- =============================================
CREATE PROCEDURE SP_CreateCobertura
	@IdPoliza uniqueidentifier,
	@IdCobertura uniqueidentifier,
	@NombreCobertura nvarchar(max),
	@ValorCobertura decimal(18,2)
	AS
	BEGIN
	SET NOCOUNT ON;

INSERT INTO [dbo].[Cobertura]
           ([IdCobertura]
           ,[NombreCobertura]
           ,[ValorCobertura]
           ,[PolizaIdPoliza])
     VALUES
           (@IdCobertura
           ,@NombreCobertura
           ,@ValorCobertura
           ,@IdPoliza)

END
GO