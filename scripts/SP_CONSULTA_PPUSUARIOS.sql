
CREATE OR REPLACE PROCEDURE DC@RNSLIB.SP_CONSULTA_PPUSUARIOS ( 
IN @IDUSER INTEGER,
IN @ACCION CHAR(1)
) 
	DYNAMIC RESULT SETS 1 
	LANGUAGE SQL 
	SPECIFIC DC@RNSLIB.SP_CONSULTA_PPUSUARIOS 
	NOT DETERMINISTIC 
	MODIFIES SQL DATA 
	CALLED ON NULL INPUT 
	SET OPTION  ALWBLK = *ALLREAD , 
	ALWCPYDTA = *OPTIMIZE , 
	COMMIT = *NONE , 
	DECRESULT = (31, 31, 00) , 
	DFTRDBCOL = QGPL , 
	DYNDFTCOL = *NO , 
	DYNUSRPRF = *USER , 
	SRTSEQ = *HEX   
	BEGIN 
DECLARE STRSQL VARCHAR ( 5000 ) ; 
DECLARE CU_01 CURSOR WITH RETURN FOR S1 ; 
SET STRSQL ='';

IF @ACCION = 'T' THEN
	SET STRSQL = 'SELECT IDUSER, USERNM, 
CASE NVLACC 
	WHEN ''ADM'' THEN ''ADMINISTRADOR''
	WHEN ''SUP'' THEN ''SUPERVISOR''
	WHEN ''US1'' THEN ''USUARIO 1''
	ELSE ''NO DEFINIDO''
END AS NVLACC,
SESTRG
FROM DC@RNSLIB.PPUSUARIO
WHERE SESTRG <> ''*''' ; 
END IF;

IF @ACCION = 'U' THEN
SET STRSQL = 'SELECT IDUSER, USERNM, 
CASE NVLACC 
	WHEN ''ADM'' THEN ''ADMINISTRADOR''
	WHEN ''SUP'' THEN ''SUPERVISOR''
	WHEN ''US1'' THEN ''USUARIO 1''
	ELSE ''NO DEFINIDO''
END AS NVLACC,
SESTRG
FROM DC@RNSLIB.PPUSUARIO
WHERE SESTRG <> ''*'' AND  IDUSER = ' || @IDUSER ; 
	 
END IF;

PREPARE S1 FROM STRSQL ; 
OPEN CU_01 ; 
  
END  ;