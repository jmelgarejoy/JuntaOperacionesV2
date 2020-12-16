--  Generar SQL 
--  Versi�n:                   	V7R1M0 100423 
--  Generado en:              	04/11/19 13:14:15 
--  Base de datos relacional:       	IASP 
--  Opci�n de est�ndares:          	DB2 for i 
SET PATH "QSYS","QSYS2","SYSPROC","SYSIBMADM","DC@DESLIB" ; 
  
CREATE PROCEDURE DC@DESLIB.SP_REGISTRAR_CABECERA_ORD_TRABAJO ( 
IN IN_NORDTR NUMERIC(10,0),
IN IN_NDTMRC NUMERIC(5,0),
IN IN_NCTZN1 NUMERIC(10,0),
IN IN_CCMPN CHAR(2),
IN IN_CDVSN CHAR(1),
IN IN_CPLNDV NUMERIC(3,0),
IN IN_CGRONG CHAR(2),
IN IN_TIPOCL CHAR(1),
IN IN_CCLNT NUMERIC(6,0),
IN IN_CCLFCT NUMERIC(6,0),
IN IN_NORDN1 NUMERIC(10,0),
IN IN_CPRTDC CHAR(6),
IN IN_NCNEM2 CHAR(30),
IN IN_NCRCNE NUMERIC(5,0),
IN IN_FCTZN NUMERIC(8,0),
IN IN_HCTZN NUMERIC(6,0),
IN IN_UCTZN CHAR(10),
IN IN_FORDTR NUMERIC(8,0),
IN IN_HORDTR NUMERIC(6,0),
IN IN_UORDTR CHAR(10),
IN IN_SESTRG CHAR(1),
IN IN_CPRCN1 CHAR(4),
IN IN_NSRCN1 CHAR(7),
IN IN_NBKNCN CHAR(20),
IN IN_FULTAC NUMERIC(8,0),
IN IN_HULTAC NUMERIC(6,0),
IN IN_CULUSA CHAR(10),
IN IN_NTRMNL CHAR(10),
IN IN_CAGADN NUMERIC(6,0),
IN IN_CDSAU1 NUMERIC(6,0),
IN IN_TORTR1 CHAR(60),
IN IN_TORTR2 CHAR(60),
IN IN_TORTR3 CHAR(60),
IN IN_TORTR4 CHAR(60),
IN IN_TORTR5 CHAR(60),
IN IN_SQNCB CHAR(1),
IN IN_SESTOT CHAR(1),
IN IN_NBKCN1 CHAR(20),
IN IN_CTPCRG CHAR(3)
 ) 
	LANGUAGE SQL 
	SPECIFIC DC@DESLIB.SP_REGISTRAR_CABECERA_ORD_TRABAJO 
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
  
INSERT INTO IASP."DC@DESLIB".RZVT01(NORDTR, NDTMRC, NCTZN1, CCMPN, CDVSN, CPLNDV, CGRONG, TIPOCL, CCLNT, CCLFCT, NORDN1, CPRTDC, NCNEM2, NCRCNE, FCTZN, HCTZN, UCTZN, FORDTR, HORDTR, UORDTR, SESTRG, CPRCN1, NSRCN1, NBKNCN, FULTAC, HULTAC, CULUSA, NTRMNL, CAGADN, CDSAU1, TORTR1, TORTR2, TORTR3, TORTR4, TORTR5, SQNCB, SESTOT, NBKCN1, CTPCRG)
VALUES(IN_NORDTR, IN_NDTMRC, IN_NCTZN1, IN_CCMPN, IN_CDVSN, IN_CPLNDV, IN_CGRONG, IN_TIPOCL, IN_CCLNT, IN_CCLFCT, IN_NORDN1, IN_CPRTDC, IN_NCNEM2, IN_NCRCNE, IN_FCTZN, IN_HCTZN, IN_UCTZN, IN_FORDTR, IN_HORDTR, IN_UORDTR, IN_SESTRG, IN_CPRCN1, IN_NSRCN1, IN_NBKNCN, IN_FULTAC, IN_HULTAC, IN_CULUSA, IN_NTRMNL, IN_CAGADN, IN_CDSAU1, IN_TORTR1, IN_TORTR2, IN_TORTR3, IN_TORTR4, IN_TORTR5, IN_SQNCB, IN_SESTOT, IN_NBKCN1, IN_CTPCRG);
  
END  ;