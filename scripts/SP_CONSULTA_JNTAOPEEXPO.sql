
CREATE OR REPLACE PROCEDURE DC@RNSLIB.SP_CONSULTA_JNTAOPEEXPO ( 
	IN IN_DESDE NUMERIC(8, 0) , 
	IN IN_HASTA NUMERIC(8, 0) ) 
	DYNAMIC RESULT SETS 1 
	LANGUAGE SQL 
	SPECIFIC DC@RNSLIB.SP_CONSULTA_JNTAOPEEXPO 
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
  
SET STRSQL = 'SELECT R22.NORSRN, R22.CVPRCN ,RZ08.TCMPVP ,
 R17.NBKNCN, CONCAT(R18.CPRCN6, R18.NSRCN6) AS CONTENE,R18.CPRCN6, R18.NSRCN6, 
R22.TOBSOR AS TPOBKN,
R18.TTMNCN, R18.CTPOC2, IFNULL(RE02.NGSLCN,0) AS NGSLCN , IFNULL(RI23.FPSDSL,0) AS FPSDSL,IFNULL(RI23.HPSDSL,0) AS HPSDSL, IFNULL(R26.FGINAL,0) AS FGINAL, IFNULL(R26.HGINAL,0) AS HGINAL,
CASE 
WHEN RE90.FLGETR IS NULL THEN ''N''
WHEN RE90.FLGETR ='''' THEN ''N''
ELSE RE90.FLGETR
END AS FLGETR, 
CASE IFNULL(RE86.CCIMO1,'''')
        WHEN ''''
        THEN
                CASE IFNULL(RE86.CCIMO2,'''')
                WHEN ''''
                THEN
                        CASE IFNULL(RE86.CCIMO3,'''')
                        WHEN ''''
                        THEN ''''
                        ELSE ''IMO''
                        END
                ELSE ''IMO''
                END
        ELSE ''IMO''
    END AS IMO,
    CASE IFNULL(RE90.NIQBF,'''')
                WHEN ''''
                THEN ''''
                ELSE ''IQBF''
                END AS IQBF,
  CASE RE21.FLGPRC
  WHEN ''D'' THEN ''DOCUMENTARIO''
  WHEN ''F'' THEN ''FISICO''
  ELSE ''''
  END AS FLGPRC,
  IFNULL(RI23.NPRGS2,'''') AS NPRGS2,
  R17.TEMBR1,
  IFNULL(RE02.NORDEM,0) AS NORDEM,
  CASE 
  WHEN RE02.NPRGI2 = '''' AND RE02.NPRGS2 = '''' THEN 0
  ELSE 
	  CASE R17.SLGLLN 
	  WHEN ''F'' THEN
	  		CASE (SELECT count(*) FROM  DC@RNSLIB.RZET01 TMP WHERE TMP.NORSRN = R22.NORSRN AND TMP.CPRCN3 = R18.CPRCN6 AND TMP.NSRCN3 = R18.NSRCN6 AND TMP.SESTRG  <> ''*'')
	  			WHEN 0 THEN IFNULL((SELECT SUM(PBLTMR)  FROM  DC@RNSLIB.RZET01 TMP WHERE TMP.NORSRN = R22.NORSRN AND TMP.CPRCN3 = R18.CPRCN6 AND TMP.NSRCN3 = R18.NSRCN6 AND TMP.SESTRG  <> ''*''),0)
	  			WHEN 1 THEN (SELECT SUM(PBLTMR)  FROM  DC@RNSLIB.RZET01 TMP WHERE TMP.NORSRN = R22.NORSRN AND TMP.CPRCN3 = R18.CPRCN6 AND TMP.NSRCN3 = R18.NSRCN6 AND TMP.SESTRG  <> ''*'' )  			
	  			ELSE IFNULL( (SELECT SUM(IFNULL(PBLTMR,0))  FROM  DC@RNSLIB.RZET01 TMP WHERE TMP.NORSRN = R22.NORSRN AND TMP.CPRCN3 = R18.CPRCN6 AND TMP.NSRCN3 = R18.NSRCN6 AND TMP.SESTRG  <> ''*''),0) + IFNULL(RE02.PTRCNT,0) 
	  			END
	  WHEN ''C'' THEN
	  		CASE IFNULL(RT42.FUNCBS,'''')
	  			WHEN ''X'' THEN (SELECT SUM(PBLTMR)  FROM  DC@RNSLIB.RZET01 TMP WHERE TMP.NORSRN = R22.NORSRN AND TMP.CPRCN3 = R18.CPRCN6 AND TMP.NSRCN3 = R18.NSRCN6 AND TMP.SESTRG  <> ''*'')  			
	  			ELSE IFNULL((SELECT SUM(IFNULL(PBLTMR,0))  FROM  DC@RNSLIB.RZET01 TMP WHERE TMP.NORSRN = R22.NORSRN AND TMP.CPRCN3 = R18.CPRCN6 AND TMP.NSRCN3 = R18.NSRCN6 AND TMP.SESTRG  <> ''*''),0) + IFNULL(RE02.PTRCNT,0) 
	  			END
	  ELSE  0
	  END 
 END AS PESO,IFNULL(RE02.PTRCNT,0) AS TARA,
  CASE RT05.FLGOPP
WHEN ''D'' THEN ''DPW''
WHEN ''A'' THEN ''APM''
ELSE ''''
END AS FLGOPP,
 IFNULL(RE07.FCOFF1,0) AS FCOFF1, IFNULL(RE07.HCOFF1,0) AS HCOFF1,
 CASE  R18.CTPCNI
  WHEN ''E'' THEN   ''SI''
  ELSE ''NO'' END AS EXCLUSIVO,
 CASE  RE02.SCNRFG
  WHEN ''S'' THEN   ''SI''
  ELSE ''NO'' END AS REFRIGER,
  RE90.SPRPRP,
  R19.QCNTSL
 FROM DC@RNSLIB.RZIT22 R22
 INNER JOIN DC@RNSLIB.RZZT17 R17 ON R22.NORSRN = R17.NORSRN  AND R17.SESTRG  <> ''*''
 INNER JOIN DC@RNSLIB.RZZT18 R18 ON R22.NORSRN = R18.NORSRN AND R17.NBKNCN = R18.NBKNCN  AND R17.SESTRG  <> ''*''
 LEFT JOIN DC@RNSLIB.RZET02 RE02 ON  R22.NORSRN = RE02.NORSRN AND R18.CPRCN6 = RE02.CPRCN4 AND R18.NSRCN6 = RE02.NSRCN4
LEFT JOIN DC@RNSLIB.RZZT19 R19 ON R22.NORSRN = R19.NORSRN AND RE02.NBKNCN = R19.NBKNCN AND RE02.TTMNCN = R19.TTMNCN AND R19.SESTRG  <> ''*''
 LEFT JOIN DC@RNSLIB.RZZK08 RZ08 ON R22.CVPRCN = RZ08.CVPRCN
 LEFT JOIN DC@RNSLIB.RZIM23 RI23 ON RE02.NGSLCN = RI23.NGASLD
 LEFT JOIN DC@RNSLIB.RZET90 RE90 ON R22.NORSRN = RE90.NORSRN AND R17.NBKNCN = RE90.NBKNCN
 LEFT JOIN DC@RNSLIB.RZET86 RE86 ON R22.NORSRN = RE86.NORSRN AND R17.NBKNCN = RE86.NBKNCN AND R18.NDTBKN = RE86.NDTBKN
 LEFT JOIN DC@RNSLIB.RZET21 RE21 ON R22.NORSRN = RE21.NORSRN AND R17.NBKNCN = RE21.NBKNCN AND R18.CPRCN6 = RE21.CPRPCN AND R18.NSRCN6 = RE21.NSRECN AND RE02.NGINCN = RE21.NGINSS
 LEFT JOIN DC@RNSLIB.RZTW42 RT42 ON R22.NORSRN = RT42.NORSRN AND R17.NBKNCN = RT42.NBKNCN  
LEFT JOIN DC@RNSLIB.RZTW05 RT05 ON R22.NORSRN = RT05.NORSRN
LEFT JOIN DC@RNSLIB.RZET08 RE08 ON R22.NORSRN = RE08.NORSRN
LEFT JOIN DC@RNSLIB.RZET07 RE07 ON RE08.NMRLQO = RE07.NMRLQO
LEFT JOIN DC@RNSLIB.RZIT26 R26 ON RE02.NGINCN = R26.NGINSS
 WHERE CTPOOP = 2 AND R22.SESTRG  <> ''*''   AND RE07.FCOFF1 BETWEEN ' || IN_DESDE || ' AND ' || IN_HASTA || '
 ORDER BY R22.NORSRN, R18.CPRCN6, R18.NSRCN6, RZ08.TCMPVP, RE90.FLGETR, RI23.FPSDSL' ; 

  
PREPARE S1 FROM STRSQL ; 
OPEN CU_01 USING IN_DESDE , IN_HASTA ; 
  
  
END  ;