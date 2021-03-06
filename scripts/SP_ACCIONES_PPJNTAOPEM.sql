
  
CREATE PROCEDURE DC@RNSLIB.SP_ACCIONES_PPJNTAOPEM ( 
IN @IDJTAOPE VARCHAR(10),
IN @FCINPLN NUMERIC(8,0),
IN @HORAINI NUMERIC(6,0),
IN @FCFNPLN NUMERIC(8,0),
IN @HORAFIN NUMERIC(6,0),
IN @CNTTUR3 INTEGER,
IN @CNTTUR1 INTEGER,
IN @CNTTUR2 INTEGER,
IN @AUTH1 VARCHAR(50),
IN @AUTH2 VARCHAR(50),
IN @AUTH3 VARCHAR(50),
IN @AUTH4 VARCHAR(50),
IN @USERCRE VARCHAR(50),
IN @FECHCRE NUMERIC(8,0),
IN @HORCRE NUMERIC(6,0),
IN @USERUPD VARCHAR(50),
IN @FECHUPD NUMERIC(8,0),
IN @HORUPD NUMERIC(6,0),
IN @ESTADO VARCHAR(1),
	IN @ACCION VARCHAR(1) ) 
	LANGUAGE SQL 
	SPECIFIC DC@RNSLIB.SP_ACCIONES_PPJNTAOPEM 
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
  
  
IF @ACCION = 'I' 
	THEN 
	INSERT INTO "DC@RNSLIB".PPJNTAOPEM(IDJTAOPE, FCINPLN, HORAINI, FCFNPLN, HORAFIN, CNTTUR3, CNTTUR1, CNTTUR2, AUTH1, AUTH2, AUTH3, AUTH4, USERCRE, FECHCRE, HORCRE, USERUPD, FECHUPD, HORUPD, ESTADO)
VALUES(@IDJTAOPE, @FCINPLN, @HORAINI, @FCFNPLN, @HORAFIN, @CNTTUR3, @CNTTUR1, @CNTTUR2, @AUTH1, @AUTH2, @AUTH3, @AUTH4, @USERCRE, @FECHCRE, @HORCRE, @USERUPD, @FECHUPD, @HORUPD, @ESTADO);
END IF ; 
  
IF @ACCION = '2' 
	THEN 
	UPDATE "DC@RNSLIB".PPJNTAOPEM 
	SET AUTH2 = @AUTH2
    WHERE IDJTAOPE = @IDJTAOPE ; 
END IF ; 

IF @ACCION = '3' 
	THEN 
	UPDATE "DC@RNSLIB".PPJNTAOPEM 
	SET AUTH3 = @AUTH3
    WHERE IDJTAOPE = @IDJTAOPE ; 
END IF ; 

IF @ACCION = '4' 
	THEN 
	UPDATE "DC@RNSLIB".PPJNTAOPEM 
	SET AUTH4 = @AUTH4
    WHERE IDJTAOPE = @IDJTAOPE ; 
END IF ; 
  
IF @ACCION = 'D' 
	THEN 
	UPDATE DC@RNSLIB . PPJNTAOPEM SET ESTADO = '*' WHERE IDJTAOPE = @IDJTAOPE ; ; 
END IF ; 
  
END  ;