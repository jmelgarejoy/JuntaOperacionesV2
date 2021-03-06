CREATE OR REPLACE PROCEDURE DC@DESLIB.SP_CONSULTA_PPMODULOS ( 
IN @IDMDLO INTEGER,
IN @ACCION CHAR(1)
) 
	DYNAMIC RESULT SETS 1 
	LANGUAGE SQL 
	SPECIFIC DC@DESLIB.SP_CONSULTA_PPMODULOS 
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
	SET STRSQL = 'SELECT IDMDLO, NMMDLO, NMMENU, PPVISTA, PPCNTRL, SESTRG
FROM DC@DESLIB.PPMODULO
WHERE SESTRG <> ''*'' ORDER BY IDMDLO, NMMENU' ; 
END IF;

IF @ACCION = 'U' THEN
SET STRSQL = 'SELECT IDMDLO, NMMDLO, NMMENU, PPVISTA, PPCNTRL, SESTRG
FROM DC@DESLIB.PPMODULO
WHERE SESTRG <> ''*'' AND  IDMDLO = ' || @IDMDLO || ' ORDER BY IDMDLO, NMMENU' ; 
END IF;




PREPARE S1 FROM STRSQL ; 
OPEN CU_01 ; 
  
END  ;