
CREATE OR REPLACE PROCEDURE  DC@RNSLIB.SP_ACCIONES_DSUSUARIO(
	IN  @IDUSER INTEGER,
    IN  @USERNM VARCHAR(20),
	IN  @NVLACC VARCHAR(3),
	IN  @SESTRG VARCHAR(1),
	IN  @ACCION VARCHAR(1)
)
BEGIN
DECLARE IDMAX INTEGER; 
SET IDMAX = (SELECT (MAX(IDUSER)+1) FROM DC@RNSLIB.DSUSUARIO);

IF @ACCION = 'I' 
	THEN 
	INSERT INTO DC@RNSLIB.DSUSUARIO(USERNM, NVLACC, SESTRG, IDUSER) VALUES(@USERNM,@NVLACC,@SESTRG, IDMAX);
END IF;

IF @ACCION = 'U' 
	THEN 
	UPDATE DC@RNSLIB.DSUSUARIO SET NVLACC = @NVLACC, SESTRG = @SESTRG WHERE IDUSER = @IDUSER;
END IF;

IF @ACCION = 'D' 
	THEN 
	UPDATE DC@RNSLIB.DSUSUARIO SET SESTRG = '*' WHERE IDUSER = @IDUSER;
END IF;

END ;
