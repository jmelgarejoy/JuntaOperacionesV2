
CREATE PROCEDURE DC@RNSLIB.SP_REPROGRAMAR_ORDEN_TRABAJO_WEB ( 
	IN @FECHA VARCHAR(8), 
	IN @ORDEN VARCHAR(20)),
	 ) 
	LANGUAGE SQL 
	SPECIFIC DC@RNSLIB.SP_REPROGRAMAR_ORDEN_TRABAJO_WEB 
	NOT DETERMINISTIC 
	MODIFIES SQL DATA 
	CALLED ON NULL INPUT 
	SET OPTION  ALWBLK = *ALLREAD , 
	ALWCPYDTA = *OPTIMIZE , 
	COMMIT = *NONE , 
	DECRESULT = (31, 31, 00) , 
	DFTRDBCOL = *NONE , 
	DYNDFTCOL = *NO , 
	DYNUSRPRF = *USER , 
	SRTSEQ = *HEX   
BEGIN 
	
	UPDATE "DC@DESLIB".RZVT02W SET FPRGOT = @FECHA WHERE NORDTR = @ORDEN ; 
	 
END 
