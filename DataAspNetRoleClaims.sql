--
-- PostgreSQL database dump
--

-- Dumped from database version 13.1
-- Dumped by pg_dump version 13.2

-- Started on 2021-05-10 15:23:29

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
-- TOC entry 3178 (class 0 OID 32469)
-- Dependencies: 202
-- Data for Name: AspNetRoleClaims; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AspNetRoleClaims" ("Id", "RoleId", "ClaimType", "ClaimValue") FROM stdin;
1	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	4
2	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	5
3	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	6
4	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	7
5	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	9
6	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	10
7	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	11
8	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	12
9	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	13
10	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	14
11	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	15
12	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	16
13	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	18
14	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	19
15	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	20
16	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	21
17	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	23
18	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	24
19	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	25
20	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	26
21	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	27
22	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	28
23	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	29
24	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	30
25	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	32
26	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	33
27	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	34
28	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	35
29	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	37
30	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	38
31	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	39
32	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	41
33	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	42
34	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	43
35	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	45
36	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	46
37	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	47
38	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	49
39	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	50
40	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	51
41	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	52
42	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	53
43	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	54
44	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	55
45	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	56
46	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	57
47	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	58
48	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	61
49	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	62
50	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	63
51	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	64
52	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	66
53	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	67
54	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	68
55	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	69
56	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	70
57	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	71
58	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	72
59	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	73
60	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	74
61	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	75
62	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	76
63	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	77
64	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	79
65	b27257ff-8f47-4c57-93e0-3d8b9aef09b4	Menu.Id	80
66	cf67cc97-1e50-4c99-91b1-e0c082636fc7	Menu.Id	82
67	cf67cc97-1e50-4c99-91b1-e0c082636fc7	Menu.Id	83
68	26789f47-908c-416d-bc95-2717821f6f19	Menu.Id	85
69	26789f47-908c-416d-bc95-2717821f6f19	Menu.Id	89
70	26789f47-908c-416d-bc95-2717821f6f19	Menu.Id	90
71	26789f47-908c-416d-bc95-2717821f6f19	Menu.Id	91
72	26789f47-908c-416d-bc95-2717821f6f19	Menu.Id	92
73	26789f47-908c-416d-bc95-2717821f6f19	Menu.Id	93
74	26789f47-908c-416d-bc95-2717821f6f19	Menu.Id	87
75	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	95
76	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	96
77	2dd6350d-3ba3-497a-82b3-e110c11af8ca	Menu.Id	97
78	cf67cc97-1e50-4c99-91b1-e0c082636fc7	Menu.Id	95
79	cf67cc97-1e50-4c99-91b1-e0c082636fc7	Menu.Id	96
80	cf67cc97-1e50-4c99-91b1-e0c082636fc7	Menu.Id	97
\.


--
-- TOC entry 3184 (class 0 OID 0)
-- Dependencies: 201
-- Name: AspNetRoleClaims_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."AspNetRoleClaims_Id_seq"', 6, true);


-- Completed on 2021-05-10 15:23:29

--
-- PostgreSQL database dump complete
--

