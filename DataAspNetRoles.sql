--
-- PostgreSQL database dump
--

-- Dumped from database version 13.1
-- Dumped by pg_dump version 13.2

-- Started on 2021-05-10 15:19:43

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
-- TOC entry 3175 (class 0 OID 32478)
-- Dependencies: 203
-- Data for Name: AspNetRoles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetRoles" ("Id", "Name", "NormalizedName", "ConcurrencyStamp") FROM stdin;
b27257ff-8f47-4c57-93e0-3d8b9aef09b4	System	SYSTEM	53ef09f0-2d8b-4dd2-abdd-f0e4702fbc22
2dd6350d-3ba3-497a-82b3-e110c11af8ca	Anonymous	ANONYMOUS	1ffacdfd-88b2-4c89-a160-7b0bf101115d
cf67cc97-1e50-4c99-91b1-e0c082636fc7	Enterprise	ENTERPRISE	6456919c-6e17-49be-9a2c-b5d17a316676
26789f47-908c-416d-bc95-2717821f6f19	Administrator	ADMINISTRATOR	2e530558-b57e-4036-acb6-c9fd9f74d7c1
\.


-- Completed on 2021-05-10 15:19:43

--
-- PostgreSQL database dump complete
--

