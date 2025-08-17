--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2025-08-15 17:25:35

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2 (class 3079 OID 16384)
-- Name: adminpack; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS adminpack WITH SCHEMA pg_catalog;


--
-- TOC entry 4811 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION adminpack; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION adminpack IS 'administrative functions for PostgreSQL';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 219 (class 1259 OID 33051)
-- Name: SaleItems; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."SaleItems" (
    "Id" uuid DEFAULT gen_random_uuid() NOT NULL,
    "ProductName" character varying(100) NOT NULL,
    "Quantity" integer NOT NULL,
    "UnitPrice" numeric(18,2) NOT NULL,
    "Discount" numeric NOT NULL,
    "SaleId" uuid NOT NULL
);


ALTER TABLE public."SaleItems" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 33037)
-- Name: Sales; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Sales" (
    "Id" uuid DEFAULT gen_random_uuid() NOT NULL,
    "SaleNumber" text NOT NULL,
    "Date" timestamp with time zone NOT NULL,
    "CustomerId" uuid NOT NULL,
    "CustomerName" character varying(100) NOT NULL,
    "BranchId" uuid NOT NULL,
    "BranchName" text NOT NULL,
    "Status" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NOT NULL
);


ALTER TABLE public."Sales" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 33045)
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    "Id" uuid DEFAULT gen_random_uuid() NOT NULL,
    "Username" character varying(50) NOT NULL,
    "Email" character varying(100) NOT NULL,
    "Phone" character varying(20) NOT NULL,
    "Password" character varying(100) NOT NULL,
    "Role" character varying(20) NOT NULL,
    "Status" character varying(20) NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 33032)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 4805 (class 0 OID 33051)
-- Dependencies: 219
-- Data for Name: SaleItems; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."SaleItems" ("Id", "ProductName", "Quantity", "UnitPrice", "Discount", "SaleId") FROM stdin;
5ca3c3de-57ac-40a7-b825-5abbf1662d28	produto1	7	2.79	1.9530	25e42e0c-6fc4-4450-acc6-05c6acce5a3b
11e4b370-12d6-4e0b-80d5-976fe873bd3f	produto2	3	5.75	0	25e42e0c-6fc4-4450-acc6-05c6acce5a3b
0c6906f0-e282-43f3-b45e-99df8ca035eb	produto2	3	5.75	0	b44e0670-688d-4670-a05e-a1838bdae695
b98bad26-dbe0-4d78-aa85-8bfffd3e2286	produto1	7	2.79	1.9530	b44e0670-688d-4670-a05e-a1838bdae695
912e4113-b83f-4f6e-af79-72bd53394329	produto1	7	2.79	1.9530	e8f23a82-a363-45d6-80ac-5a92eae05fb0
0e5c428c-b0e1-4462-8633-c00e67b4513e	produto3	3	5.75	0	e8f23a82-a363-45d6-80ac-5a92eae05fb0
e27dbf34-fa33-43ae-ba8e-48badca08325	produto3	3	5.75	0	bded39d0-aa8c-40b9-a77b-6ba8b5f080dc
fcbe6883-6939-41a8-b328-d6af5c91558a	produto4	7	2.79	1.9530	bded39d0-aa8c-40b9-a77b-6ba8b5f080dc
fa32f881-93ba-47a7-b78c-9199638d5c21	produto4	7	2.79	1.9530	820a3d29-76be-4ebc-9512-7a74138df6be
21f38eab-6a78-4cfd-819f-6481191edf54	produto5	3	5.75	0	820a3d29-76be-4ebc-9512-7a74138df6be
c1e6e65c-5418-4354-88c1-4c5b46b50475	produto6	7	2.79	1.9530	aa77bb74-d6ec-4e65-a107-ccbf477a1908
64eca088-fdfb-4296-ba65-63b66a4772ba	produto5	3	5.75	0	aa77bb74-d6ec-4e65-a107-ccbf477a1908
791b8fbf-c3d6-457e-a9e5-2dd394d5b73c	produto6	7	2.79	1.9530	a65b7621-41ad-49ec-8152-e2452b71defb
5160f0e1-364f-4339-9d46-ffa3bc2d4c82	produto7	3	5.75	0	a65b7621-41ad-49ec-8152-e2452b71defb
b380ec5d-bf5c-4848-8b11-d986c583ac2c	produto8	7	2.79	1.9530	42d5a554-e9f9-410f-97f1-af50036bcec1
0ff0afd9-a3a1-4b4b-b40c-33fffb716a4d	produto7	3	5.75	0	42d5a554-e9f9-410f-97f1-af50036bcec1
35b8dbf7-84a7-4343-aa3b-2b0940a66d93	produto9	3	5.75	0	9eb6d5c6-2739-414e-a297-5d987c7ca3b1
18257cd7-8142-4b09-8b11-baec498a5c5c	produto8	7	2.79	1.9530	9eb6d5c6-2739-414e-a297-5d987c7ca3b1
08c57456-a9bf-465d-be9d-2fbcf9f4675a	produto9	3	5.75	0	1cb32f9a-b001-4ef2-859f-b5b1b22c0c7b
7c8f18a3-f6e7-4890-9332-c807415eb442	produto10	7	2.79	1.9530	1cb32f9a-b001-4ef2-859f-b5b1b22c0c7b
ff2fafd8-3e86-4f67-87c3-2605ccc663ba	produto10	7	2.79	1.9530	7f59ceff-8e79-4ebd-9630-ea367a67065c
1e1729a4-6d3e-4e7a-9122-be25ae891dfb	produto11	3	5.75	0	7f59ceff-8e79-4ebd-9630-ea367a67065c
3bc7555b-7521-4b3d-b536-1d63246e38eb	produto10	7	2.79	1.9530	3bda452e-3445-4664-aa7b-3e32f65cd7e0
b3813507-5212-43b4-9970-e6f13a805128	produto11	3	5.75	0	3bda452e-3445-4664-aa7b-3e32f65cd7e0
dc183c21-cb77-4522-b61c-bf2ac0968651	produto11	3	5.75	0	0bd86a95-a371-4c34-a117-45104a480b4f
6c69edf0-3283-404e-8047-cfac479c00cf	produto10	7	2.79	1.9530	0bd86a95-a371-4c34-a117-45104a480b4f
33eca958-77f2-4c30-9d8f-69707c28fadb	produto11	3	5.75	0	468e4dc3-0539-47fd-add7-9a963d7b3326
edc61688-88ad-4d33-8d8f-54c64391ff11	produto1	7	2.79	1.9530	468e4dc3-0539-47fd-add7-9a963d7b3326
77dcba04-dbdf-4b76-ba07-dcc18b8f196f	produto1	7	2.79	1.9530	8080e87e-03f9-443e-a811-07b77e29ce69
fb8d3c51-4475-4d33-8531-dded77d9152e	produto17	3	5.75	0	8080e87e-03f9-443e-a811-07b77e29ce69
d3b3673d-deb2-4fe1-97ab-69eef601e903	produto1	7	2.79	1.9530	7825893c-3899-49e8-8805-f3ed68d40ed9
73b50407-f654-47ff-973d-793a14dc095f	produto13	3	5.75	0	7825893c-3899-49e8-8805-f3ed68d40ed9
fd6348dd-a3a5-495e-ae8b-f0c95006fd30	produto13	3	5.75	0	c0022962-2ce8-4f7d-98f0-a45d0cbba24a
3570b126-32c8-4465-8915-5af5282d8ace	produto1	7	2.79	1.9530	c0022962-2ce8-4f7d-98f0-a45d0cbba24a
7e87070d-786b-4b8a-8074-db6f3fafdc36	produto12	3	5.75	0	259e4b78-ff37-4d38-bf00-2fa7ecfef8c5
cd7bbfd1-580c-49f0-ba45-2841f0bbc6ec	produto1	7	2.79	1.9530	259e4b78-ff37-4d38-bf00-2fa7ecfef8c5
757da1ec-99fd-476e-9533-7a5befd97ab5	produto1	7	2.79	1.9530	b6f70bd5-490d-4044-b3ec-59ff564d05f3
bc420a2d-ec1d-46be-8b38-f3f43befef9f	produto14	3	5.75	0	b6f70bd5-490d-4044-b3ec-59ff564d05f3
60244864-ca7d-46c0-9f71-a4c4f38b7966	produto1	7	2.79	1.9530	938c6411-0f1b-430c-87ea-25c99e788127
a35d6e97-dc50-4d60-925b-37de355776ac	produto15	3	5.75	0	938c6411-0f1b-430c-87ea-25c99e788127
210831f0-832d-4993-9096-f315e0224a17	produto15	3	5.75	0	47d6c48b-d94d-4574-8aeb-c54c7ec11eb5
c46a4b3b-4b85-4c39-a180-cce3a8c4e386	produto19	7	2.79	1.9530	47d6c48b-d94d-4574-8aeb-c54c7ec11eb5
1033fd69-2073-4742-a968-993a65e1250a	produto19	7	2.79	1.9530	0bbc337c-783e-48fb-9577-6579c783b068
ecbfe083-5544-4fa9-bdab-a2d4365e823a	produto1	3	5.75	0	0bbc337c-783e-48fb-9577-6579c783b068
024bb0db-2a36-4d1f-87cb-6997b2b6e91f	produto1	3	5.75	0	1a0aac85-55c1-437d-8096-fb85e4a47c97
8eadc157-401f-4787-aedd-d9632773156b	produto21	7	2.79	1.9530	1a0aac85-55c1-437d-8096-fb85e4a47c97
5c1fdd13-68b4-4dbd-ac47-206fd6b88699	produto21	7	2.79	1.9530	cd890ffb-8e81-4391-9d05-713dcb2a9fe3
a04265a1-7c18-447a-8097-75ef523f5058	produto12	3	5.75	0	cd890ffb-8e81-4391-9d05-713dcb2a9fe3
db0467b8-9be3-41eb-ad32-71150c10005f	produto21	7	2.79	1.9530	f088f73a-f35b-4543-9892-567eef8a23d7
69b2b1cf-f01a-4558-9c11-52cb3baed184	produto13	3	5.75	0	f088f73a-f35b-4543-9892-567eef8a23d7
dc601ece-c68f-4110-92ad-aed6676dd2fa	produto14	3	5.75	0	229ab582-2a27-4df5-a035-3ee114635762
ef6b5afc-5810-430d-aba9-0a210f728b57	produto21	7	2.79	1.9530	229ab582-2a27-4df5-a035-3ee114635762
54aff4a1-3164-4abb-acbf-fdbfe22b9ab7	produto21	7	2.79	1.9530	bf488bd7-df56-4671-8fe0-0212bf73f1dd
723eeb6a-2547-4025-89aa-fbe7d3f40f70	produto15	3	5.75	0	bf488bd7-df56-4671-8fe0-0212bf73f1dd
ec400e50-fe58-4114-bf09-09fa10b36035	produto16	3	5.75	0	1ae28bd6-fff2-4ead-a490-1716f202e2d7
2119ce26-12a0-4905-ba49-0fa8dcbd1de8	produto21	7	2.79	1.9530	1ae28bd6-fff2-4ead-a490-1716f202e2d7
086f8636-a6e5-4f9f-9694-4f8fec99c37f	produto2	3	5.75	0	9ce4f2a9-1c10-4a22-8ece-174adb7eeece
fe198203-37e5-410e-a0fd-b6c4f3fe8e90	produto1	7	2.79	1.9530	9ce4f2a9-1c10-4a22-8ece-174adb7eeece
\.


--
-- TOC entry 4803 (class 0 OID 33037)
-- Dependencies: 217
-- Data for Name: Sales; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Sales" ("Id", "SaleNumber", "Date", "CustomerId", "CustomerName", "BranchId", "BranchName", "Status", "CreatedAt", "UpdatedAt") FROM stdin;
25e42e0c-6fc4-4450-acc6-05c6acce5a3b	string	2025-08-14 12:12:42.092011-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	1	2025-08-14 12:12:43.368779-03	2025-08-14 13:06:26.724914-03
b44e0670-688d-4670-a05e-a1838bdae695	2025-000002	2025-08-14 13:53:46.110084-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:53:50.921891-03	2025-08-14 13:53:50.921891-03
e8f23a82-a363-45d6-80ac-5a92eae05fb0	2025-000003	2025-08-14 13:54:11.758077-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:54:11.80007-03	2025-08-14 13:54:11.80007-03
bded39d0-aa8c-40b9-a77b-6ba8b5f080dc	2025-000004	2025-08-14 13:54:20.307558-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:54:20.313338-03	2025-08-14 13:54:20.313338-03
820a3d29-76be-4ebc-9512-7a74138df6be	2025-000005	2025-08-14 13:54:26.115951-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:54:26.120286-03	2025-08-14 13:54:26.120286-03
aa77bb74-d6ec-4e65-a107-ccbf477a1908	2025-000006	2025-08-14 13:54:34.400845-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:54:34.40481-03	2025-08-14 13:54:34.40481-03
a65b7621-41ad-49ec-8152-e2452b71defb	2025-000007	2025-08-14 13:54:44.196343-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:54:44.200034-03	2025-08-14 13:54:44.200034-03
42d5a554-e9f9-410f-97f1-af50036bcec1	2025-000008	2025-08-14 13:54:48.660709-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:54:48.664646-03	2025-08-14 13:54:48.664646-03
9eb6d5c6-2739-414e-a297-5d987c7ca3b1	2025-000009	2025-08-14 13:54:52.827054-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:54:52.831042-03	2025-08-14 13:54:52.831042-03
1cb32f9a-b001-4ef2-859f-b5b1b22c0c7b	2025-000010	2025-08-14 13:54:59.633789-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:54:59.637408-03	2025-08-14 13:54:59.637408-03
7f59ceff-8e79-4ebd-9630-ea367a67065c	2025-000011	2025-08-14 13:55:05.311984-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:55:05.31625-03	2025-08-14 13:55:05.31625-03
3bda452e-3445-4664-aa7b-3e32f65cd7e0	2025-000012	2025-08-14 13:55:20.60579-03	e34abfa8-25c1-449f-bc18-afb04035c62f	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:55:20.609276-03	2025-08-14 13:55:20.609276-03
0bd86a95-a371-4c34-a117-45104a480b4f	2025-000013	2025-08-14 13:57:42.9786-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:57:42.984923-03	2025-08-14 13:57:42.984923-03
468e4dc3-0539-47fd-add7-9a963d7b3326	2025-000014	2025-08-14 13:57:55.855773-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:57:55.859943-03	2025-08-14 13:57:55.859943-03
8080e87e-03f9-443e-a811-07b77e29ce69	2025-000015	2025-08-14 13:58:02.25582-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:58:02.259436-03	2025-08-14 13:58:02.259436-03
7825893c-3899-49e8-8805-f3ed68d40ed9	2025-000016	2025-08-14 13:58:06.583095-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:58:06.589229-03	2025-08-14 13:58:06.589229-03
c0022962-2ce8-4f7d-98f0-a45d0cbba24a	2025-000017	2025-08-14 13:58:09.31221-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:58:09.315966-03	2025-08-14 13:58:09.315966-03
259e4b78-ff37-4d38-bf00-2fa7ecfef8c5	2025-000018	2025-08-14 13:58:14.93391-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:58:14.938391-03	2025-08-14 13:58:14.938391-03
b6f70bd5-490d-4044-b3ec-59ff564d05f3	2025-000019	2025-08-14 13:58:20.154365-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:58:20.158029-03	2025-08-14 13:58:20.158029-03
938c6411-0f1b-430c-87ea-25c99e788127	2025-000020	2025-08-14 13:58:27.150591-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:58:27.154648-03	2025-08-14 13:58:27.154648-03
47d6c48b-d94d-4574-8aeb-c54c7ec11eb5	2025-000021	2025-08-14 13:58:35.646934-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:58:35.650607-03	2025-08-14 13:58:35.650607-03
0bbc337c-783e-48fb-9577-6579c783b068	2025-000022	2025-08-14 13:58:42.31797-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:58:42.321405-03	2025-08-14 13:58:42.321405-03
1a0aac85-55c1-437d-8096-fb85e4a47c97	2025-000023	2025-08-14 13:58:50.406994-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:58:50.446905-03	2025-08-14 13:58:50.446905-03
cd890ffb-8e81-4391-9d05-713dcb2a9fe3	2025-000024	2025-08-14 13:58:54.063919-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:58:54.067799-03	2025-08-14 13:58:54.067799-03
f088f73a-f35b-4543-9892-567eef8a23d7	2025-000025	2025-08-14 13:59:00.792828-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:59:00.796587-03	2025-08-14 13:59:00.796587-03
229ab582-2a27-4df5-a035-3ee114635762	2025-000026	2025-08-14 13:59:04.89273-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:59:04.8964-03	2025-08-14 13:59:04.8964-03
bf488bd7-df56-4671-8fe0-0212bf73f1dd	2025-000027	2025-08-14 13:59:09.964933-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:59:09.968741-03	2025-08-14 13:59:09.968741-03
1ae28bd6-fff2-4ead-a490-1716f202e2d7	2025-000028	2025-08-14 13:59:13.796835-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-14 13:59:13.800285-03	2025-08-14 13:59:13.800285-03
9ce4f2a9-1c10-4a22-8ece-174adb7eeece	2025-000029	2025-08-15 13:14:39.391364-03	1fcece07-4fe1-4394-9775-a92fccdda833	string	1fcece07-4fe1-4394-9775-a92fccdda833	gs7	0	2025-08-15 13:14:39.41954-03	2025-08-15 13:14:39.419541-03
\.


--
-- TOC entry 4804 (class 0 OID 33045)
-- Dependencies: 218
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Users" ("Id", "Username", "Email", "Phone", "Password", "Role", "Status", "CreatedAt", "UpdatedAt") FROM stdin;
e34abfa8-25c1-449f-bc18-afb04035c62f	gs2	strin@fg.gg	451456254	$2a$11$r3IM4H4U7d3XmyHT6orJY.qs8ri6SgeafJetc5WwdcArRPeCr/rGq	Customer	Active	2025-08-14 09:35:45.181956-03	2025-08-14 10:11:35.750593-03
1fcece07-4fe1-4394-9775-a92fccdda833	gs7	teste@mail.com	451456254	$2a$11$2V59IXblIeVJQe5l9aq4/Oa6ILJNERX1ST6.oohasaTszYknHLNsG	Admin	Active	2025-08-14 09:34:51.14698-03	\N
\.


--
-- TOC entry 4802 (class 0 OID 33032)
-- Dependencies: 216
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20250810204131_Sales	8.0.10
20250810210540_SalesV2	8.0.10
\.


--
-- TOC entry 4657 (class 2606 OID 33057)
-- Name: SaleItems PK_SaleItems; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SaleItems"
    ADD CONSTRAINT "PK_SaleItems" PRIMARY KEY ("Id");


--
-- TOC entry 4652 (class 2606 OID 33044)
-- Name: Sales PK_Sales; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Sales"
    ADD CONSTRAINT "PK_Sales" PRIMARY KEY ("Id");


--
-- TOC entry 4654 (class 2606 OID 33050)
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");


--
-- TOC entry 4650 (class 2606 OID 33036)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 4655 (class 1259 OID 33063)
-- Name: IX_SaleItems_SaleId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_SaleItems_SaleId" ON public."SaleItems" USING btree ("SaleId");


--
-- TOC entry 4658 (class 2606 OID 33058)
-- Name: SaleItems FK_SaleItems_Sales_SaleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SaleItems"
    ADD CONSTRAINT "FK_SaleItems_Sales_SaleId" FOREIGN KEY ("SaleId") REFERENCES public."Sales"("Id") ON DELETE CASCADE;


-- Completed on 2025-08-15 17:25:35

--
-- PostgreSQL database dump complete
--

-- Insere manualmente as migrations que já estão aplicadas pelo dump
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES
('20250810204131_Sales', '8.0.10'),
('20250810210540_SalesV2', '8.0.10');
