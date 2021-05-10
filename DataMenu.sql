--
-- PostgreSQL database dump
--

-- Dumped from database version 13.1
-- Dumped by pg_dump version 13.2

-- Started on 2021-05-10 15:21:41

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
-- TOC entry 3178 (class 0 OID 35574)
-- Dependencies: 267
-- Data for Name: Menu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Menu" ("Id", "ParentId", "Type", "Text", "Small", "BadgeText", "BadgeClass", "Url", "Icon") FROM stdin;
1	\N	0	\N	\N	\N	\N	\N	\N
5	2	2	Dashboard v2	\N	\N	\N	../../index2.html	far fa-circle
6	2	2	Dashboard v3	\N	\N	\N	../../index3.html	far fa-circle
7	1	2	Widgets	\N	New	badge-danger	../widgets.html	fas fa-th
8	1	1	Layout Options	\N	6	badge-info	\N	fas fa-copy
9	8	2	Top Navigation	\N	\N	\N	../layout/top-nav.html	far fa-circle
10	8	2	Top Navigation + Sidebar	\N	\N	\N	../layout/top-nav-sidebar.html	far fa-circle
40	\N	0	EXAMPLES	\N	\N	\N	\N	\N
81	\N	0	MISCELLANEOUS	\N	\N	\N	\N	\N
84	\N	0	MULTI LEVEL EXAMPLE	\N	\N	\N	\N	\N
11	8	2	Boxed	\N	\N	\N	../layout/boxed.html	far fa-circle
12	8	2	Fixed Sidebar	\N	\N	\N	../layout/fixed-sidebar.html	far fa-circle
13	8	2	Fixed Sidebar	+ Custom Area	\N	\N	../layout/fixed-sidebar-custom.html	far fa-circle
14	8	2	Fixed Navbar	\N	\N	\N	../layout/fixed-topnav.html	far fa-circle
15	8	2	Fixed Footer	\N	\N	\N	../layout/fixed-footer.html	far fa-circle
17	1	1	Charts	\N	\N	\N	\N	fas fa-chart-pie
18	17	2	ChartJS	\N	\N	\N	../charts/chartjs.html	far fa-circle
19	17	2	Flot	\N	\N	\N	../charts/flot.html	far fa-circle
20	17	2	Inline	\N	\N	\N	../charts/inline.html	far fa-circle
21	17	2	uPlot	\N	\N	\N	../charts/uplot.html	far fa-circle
22	1	1	UI Elements	\N	\N	\N	\N	fas fa-tree
23	22	2	General	\N	\N	\N	../UI/general.html	far fa-circle
24	22	2	Icons	\N	\N	\N	../UI/icons.html	far fa-circle
25	22	2	Buttons	\N	\N	\N	../UI/buttons.html	far fa-circle
26	22	2	Sliders	\N	\N	\N	../UI/sliders.html	far fa-circle
27	22	2	Modals & Alerts	\N	\N	\N	../UI/modals.html	far fa-circle
28	22	2	Navbar & Tabs	\N	\N	\N	../UI/navbar.html	far fa-circle
29	22	2	Timeline	\N	\N	\N	../UI/timeline.html	far fa-circle
30	22	2	Ribbons	\N	\N	\N	../UI/ribbons.html	far fa-circle
31	1	1	Forms	\N	\N	\N	\N	fas fa-edit
33	31	2	Advanced Elements	\N	\N	\N	../forms/advanced.html	far fa-circle
34	31	2	Editors	\N	\N	\N	../forms/editors.html	far fa-circle
35	31	2	Validation	\N	\N	\N	../forms/validation.html	far fa-circle
36	1	1	Tables	\N	\N	\N	\N	fas fa-table
37	36	2	Simple Tables	\N	\N	\N	../tables/simple.html	far fa-circle
38	36	2	DataTables	\N	\N	\N	../tables/data.html	far fa-circle
39	36	2	jsGrid	\N	\N	\N	../tables/jsgrid.html	far fa-circle
43	40	2	Kanban Board	\N	\N	\N	../kanban.html	fas fa-columns
78	40	1	Search	\N	\N	\N	\N	fas fa-search
83	81	2	Documentation	\N	\N	\N	https://adminlte.io/docs/3.1/	fas fa-file
44	40	1	Mailbox	\N	\N	\N	\N	fas fa-envelope
45	44	2	Inbox	\N	\N	\N	../mailbox/mailbox.html	far fa-circle
46	44	2	Compose	\N	\N	\N	../mailbox/compose.html	far fa-circle
48	40	1	Pages	\N	\N	\N	\N	fas fa-book
49	48	2	Invoice	\N	\N	\N	../examples/invoice.html	far fa-circle
50	48	2	Profile	\N	\N	\N	../examples/profile.html	far fa-circle
51	48	2	E-commerce	\N	\N	\N	../examples/e-commerce.html	far fa-circle
52	48	2	Projects	\N	\N	\N	../examples/projects.html	far fa-circle
53	48	2	Project Add	\N	\N	\N	../examples/project-add.html	far fa-circle
54	48	2	Project Edit	\N	\N	\N	../examples/project-edit.html	far fa-circle
56	48	2	Contacts	\N	\N	\N	../examples/contacts.html	far fa-circle
57	48	2	FAQ	\N	\N	\N	../examples/faq.html	far fa-circle
58	48	2	Contact us	\N	\N	\N	../examples/contact-us.html	far fa-circle
59	40	1	Extras	\N	\N	\N	\N	fas fa-plus-square
60	59	1	Login & Register v1	\N	\N	\N	\N	far fa-circle
61	60	2	Login v1	\N	\N	\N	../examples/login.html	far fa-circle
62	60	2	Register v1	\N	\N	\N	../examples/register.html	far fa-circle
63	60	2	Forgot Password v1	\N	\N	\N	../examples/forgot-password.html	far fa-circle
64	60	2	Recover Password v1	\N	\N	\N	../examples/recover-password.html	far fa-circle
65	59	1	Login & Register v2	\N	\N	\N	\N	far fa-circle
66	65	2	Login v2	\N	\N	\N	../examples/login-v2.html	far fa-circle
68	65	2	Forgot Password v2	\N	\N	\N	../examples/forgot-password-v2.html	far fa-circle
69	65	2	Recover Password v2	\N	\N	\N	../examples/recover-password-v2.html	far fa-circle
70	59	2	Lockscreen	\N	\N	\N	../examples/lockscreen.html	far fa-circle
71	59	2	Legacy User Menu	\N	\N	\N	../examples/legacy-user-menu.html	far fa-circle
72	59	2	Language Menu	\N	\N	\N	../examples/language-menu.html	far fa-circle
73	59	2	Error 404	\N	\N	\N	../examples/404.html	far fa-circle
74	59	2	Error 500	\N	\N	\N	../examples/500.html	far fa-circle
75	59	2	Pace	\N	\N	\N	../examples/pace.html	far fa-circle
76	59	2	Blank Page	\N	\N	\N	../examples/blank.html	far fa-circle
77	59	2	Starter Page	\N	\N	\N	../../starter.html	far fa-circle
41	40	2	Calendar	\N	2	badge-info	../calendar.html	fas fa-calendar-alt
79	78	2	Simple Search	\N	\N	\N	../search/simple.html	far fa-circle
80	78	2	Enhanced	\N	\N	\N	../search/enhanced.html	far fa-circle
93	84	2	Level 1	\N	\N	\N	#	fas fa-circle
86	84	1	Level 1	\N	\N	\N	\N	fas fa-circle
87	86	2	Level 2	\N	\N	\N	#	far fa-circle
88	86	1	Level 2	\N	\N	\N	\N	far fa-circle
89	88	2	Level 3	\N	\N	\N	#	far fa-dot-circle
90	88	2	Level 3	\N	\N	\N	#	far fa-dot-circle
91	88	2	Level 3	\N	\N	\N	#	far fa-dot-circle
92	86	2	Level 2	\N	\N	\N	#	far fa-circle
85	84	2	Level 1	\N	\N	\N	#	fas fa-circle
42	40	2	Gallery	\N	\N	\N	../gallery.html	fas fa-image
94	\N	0	LABELS	\N	\N	\N	\N	\N
2	1	1	Dashboard	\N	\N	\N	\N	fas fa-tachometer-alt
4	2	2	Dashboard v1	\N	\N	\N	../../index.html	far fa-circle
16	8	2	Collapsed Sidebar	\N	\N	\N	../layout/collapsed-sidebar.html	far fa-circle
32	31	2	General Elements	\N	\N	\N	../forms/general.html	far fa-circle
47	44	2	Read	\N	\N	\N	../mailbox/read-mail.html	far fa-circle
55	48	2	Project Detail	\N	\N	\N	../examples/project-detail.html	far fa-circle
67	65	2	Register v2	\N	\N	\N	../examples/register-v2.html	far fa-circle
82	81	2	Tabbed IFrame Plugin	\N	\N	\N	../../iframe.html	fas fa-ellipsis-h
97	94	2	Informational	\N	\N	\N	#	fas fa-circle text-info
95	94	2	Important	\N	\N	\N	#	fas fa-circle text-danger
96	94	2	Warning	\N	\N	\N	#	fas fa-circle text-warning
\.


--
-- TOC entry 3184 (class 0 OID 0)
-- Dependencies: 266
-- Name: Menu_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Menu_Id_seq"', 97, true);


-- Completed on 2021-05-10 15:21:41

--
-- PostgreSQL database dump complete
--

