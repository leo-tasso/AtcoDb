-- *********************************************
-- * SQL MySQL generation                      
-- *--------------------------------------------
-- * DB-MAIN version: 11.0.2              
-- * Generator date: Sep 14 2021              
-- * Generation date: Wed May 17 17:26:58 2023 
-- * LUN file: C:\Users\Leonardo\OneDrive - Alma Mater Studiorum Università di Bologna\Exercises\ATC Personnel.lun 
-- * Schema: AtcTables/1 
-- ********************************************* 


-- Database Section
-- ________________ 

create database AtcTables;
use AtcTables;


-- Tables Section
-- _____________ 

create table Abilitazione (
     MatricolaAbilitazione int not null,
     IdControllore char(10) not null,
     constraint IDAbilitazione_ID primary key (MatricolaAbilitazione));

create table AbilitazioneSettori (
     MatricolaAbilitazione int not null,
     IdSettore char(50) not null,
     constraint IDAbilitazioneSettori primary key (MatricolaAbilitazione, IdSettore));

create table Aereomobile (
     Tipo char(4) not null,
     NumeroDiCoda char(6) not null,
     constraint IDAereomobile primary key (NumeroDiCoda));

create table Aerodromo (
     AdLatitudine float not null,
     AdLongitudine float not null,
     CodiceIcao char(4) not null,
     CodiceIata char(3) not null,
     constraint IDAerodromo_ID primary key (CodiceIcao));

create table Centro (
     NomeCentro varchar(40) not null,
     constraint IDCentro primary key (NomeCentro));

create table ComposizioneSettori (
     IdPostazione varchar(50) not null,
     IdSettore char(50) not null,
     constraint IDComposizione primary key (IdPostazione, IdSettore));

create table Controllore (
     IdControllore char(10) not null,
     Nome varchar(20) not null,
     Cognome varchar(20) not null,
     NomeCentro varchar(40) not null,
     constraint IDControllore primary key (IdControllore));

create table Ferie (
     IdControllore char(10) not null,
     Inizio date not null,
     Fine date not null,
     constraint IDFerie primary key (IdControllore, Inizio));

create table Percorrenza (
     Callsign varchar(30) not null,
     Dof date not null,
     NomePunto char(5) not null,
     OrarioDiSorvolo datetime not null,
     constraint IDPercorrenza primary key (Callsign, Dof, NomePunto));

create table PianoDiVolo (
     OrarioAtterraggio datetime,
     OrarioDecollo datetime,
     Callsign varchar(30) not null,
     Dof date not null,
     NumeroDiCoda char(6) not null,
     CodAdDecollo char(4) not null,
     OrientamentoPistaDecollo char(3) not null,
     CodAdAtterraggio char(4) not null,
     OrientamentoPistaAtterraggio char(3) not null,
     constraint IDPianoDiVolo primary key (Callsign, Dof));

create table Pista (
     CodAd char(4) not null,
     Orientamento char(3) not null,
     Lunghezza  int not null,
     constraint IDPista primary key (CodAd, Orientamento));

create table Postazione (
     IdPostazione varchar(50) not null,
     NomeCentro varchar(40) not null,
     constraint IDPostazione_ID primary key (IdPostazione));

create table Punto (
     NomePunto char(5) not null,
     PosLatitudine float not null,
     PosLongitudine float not null,
     IdSettore char(50) not null,
     constraint IDPunto primary key (NomePunto));

create table Settore (
     IdSettore char(50) not null,
     CodAd char(4),
     constraint IDSettore_ID primary key (IdSettore));

create table Stimati (
     Callsign varchar(30) not null,
     Dof date not null,
     NomePunto char(5) not null,
     OrarioStimato datetime not null,
     constraint IDStimati primary key (Callsign, Dof, NomePunto));

create table Turno (
     IdControllore char(10) not null,
     Retribuzione int not null,
     Data date not null,
     Slot int not null,
     IdPostazione varchar(50),
     CentroStandBy varchar(50),
     constraint IDTurno primary key (IdControllore, Data, Slot));


-- Constraints Section
-- ___________________ 

-- Not implemented
-- alter table Abilitazione add constraint IDAbilitazione_CHK
--     check(exists(select * from AbilitazioneSettori
--                  where AbilitazioneSettori.MatricolaAbilitazione = MatricolaAbilitazione)); 

alter table Abilitazione add constraint FKPossedimento
     foreign key (IdControllore)
     references Controllore (IdControllore);

alter table AbilitazioneSettori add constraint FKIdSettore
     foreign key (IdSettore)
     references Settore (IdSettore);

alter table AbilitazioneSettori add constraint FKMatricolaAbilitazione
     foreign key (MatricolaAbilitazione)
     references Abilitazione (MatricolaAbilitazione);

-- Not implemented
-- alter table Aerodromo add constraint IDAerodromo_CHK
--     check(exists(select * from Settore
--                  where Settore.CodAd = CodiceIcao)); 

alter table ComposizioneSettori add constraint FKCom_Set
     foreign key (IdSettore)
     references Settore (IdSettore);

alter table ComposizioneSettori add constraint FKCom_Pos
     foreign key (IdPostazione)
     references Postazione (IdPostazione);

alter table Controllore add constraint FKAssegnazione
     foreign key (NomeCentro)
     references Centro (NomeCentro);

alter table Ferie add constraint FKSvolgimento
     foreign key (IdControllore)
     references Controllore (IdControllore);

alter table Percorrenza add constraint FKPer_Pun
     foreign key (NomePunto)
     references Punto (NomePunto);

alter table Percorrenza add constraint FKPer_Pia
     foreign key (Callsign, Dof)
     references PianoDiVolo (Callsign, Dof);

alter table PianoDiVolo add constraint FKPraticamento
     foreign key (NumeroDiCoda)
     references Aereomobile (NumeroDiCoda);

alter table PianoDiVolo add constraint FKPistaDecollo
     foreign key (CodAdDecollo, OrientamentoPistaDecollo)
     references Pista (CodAd, Orientamento);

alter table PianoDiVolo add constraint FKPistaAtterraggio
     foreign key (CodAdAtterraggio, OrientamentoPistaAtterraggio)
     references Pista (CodAd, Orientamento);

alter table Pista add constraint FKComposizione 
     foreign key (CodAd)
     references Aerodromo (CodiceIcao);

-- Not implemented
-- alter table Postazione add constraint IDPostazione_CHK
--     check(exists(select * from ComposizioneSettori
--                  where ComposizioneSettori.IdPostazione = IdPostazione)); 

alter table Postazione add constraint FKUbicazione
     foreign key (NomeCentro)
     references Centro (NomeCentro);

alter table Punto add constraint FKR
     foreign key (IdSettore)
     references Settore (IdSettore);

-- Not implemented
-- alter table Settore add constraint IDSettore_CHK
--     check(exists(select * from ComposizioneSettori
--                  where ComposizioneSettori.IdSettore = IdSettore)); 

alter table Settore add constraint FKAppartenenza  
     foreign key (CodAd)
     references Aerodromo (CodiceIcao);

alter table Stimati add constraint FKSti_Pun
     foreign key (NomePunto)
     references Punto (NomePunto);

alter table Stimati add constraint FKSti_Pia
     foreign key (Callsign, Dof)
     references PianoDiVolo (Callsign, Dof);

alter table Turno add constraint FKTurnoLavorativo
     foreign key (IdPostazione)
     references Postazione (IdPostazione);

alter table Turno add constraint FKTurnoStandBy
     foreign key (CentroStandby)
     references Centro (NomeCentro);

alter table Turno add constraint FKLavora
     foreign key (IdControllore)
     references Controllore (IdControllore);


-- Index Section
-- _____________ 

