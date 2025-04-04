USE [Language_Center_Management2]
GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 03/31/2025 08:50:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignment](
	[Assignment_Code] [varchar](50) NOT NULL,
	[Description] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Assignment_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[Course_ID] [varchar](50) NOT NULL,
	[Attendance_Sheet_Link] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Course_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classroom]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classroom](
	[Room_No] [varchar](50) NOT NULL,
	[Capacity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Room_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[Course_ID] [varchar](50) NOT NULL,
	[Course_Name] [varchar](50) NULL,
	[Language_Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Course_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course_Schedule]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course_Schedule](
	[Course_ID] [varchar](50) NOT NULL,
	[Starting_Date] [date] NULL,
	[Ending_Date] [date] NULL,
	[Room_No] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Course_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[Assignment_Code] [varchar](50) NOT NULL,
	[Student_ID] [varchar](50) NOT NULL,
	[Course_ID] [varchar](50) NOT NULL,
	[Assignment_Date] [date] NULL,
	[Assignment_Grade] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[Course_ID] ASC,
	[Student_ID] ASC,
	[Assignment_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Language_Name] [varchar](50) NOT NULL,
	[Description] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Language_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prerequisites]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prerequisites](
	[Course_ID] [varchar](50) NOT NULL,
	[Preq_ID] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Course_ID] ASC,
	[Preq_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Protector]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Protector](
	[Student_ID] [varchar](50) NOT NULL,
	[Protector_Name] [varchar](50) NULL,
	[Protector_Phone] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Student_ID] ASC,
	[Protector_Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[Course_ID] [varchar](50) NOT NULL,
	[DayofWeek] [varchar](50) NOT NULL,
	[Starting_Time] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Course_ID] ASC,
	[DayofWeek] ASC,
	[Starting_Time] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule_Time]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule_Time](
	[DayofWeek] [varchar](50) NOT NULL,
	[Starting_Time] [varchar](50) NOT NULL,
	[Ending_Time] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[DayofWeek] ASC,
	[Starting_Time] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Student_ID] [varchar](50) NOT NULL,
	[Student_Name] [varchar](50) NULL,
	[Student_Phone] [varchar](50) NULL,
	[Student_Email] [varchar](50) NULL,
	[Student_DOB] [date] NULL,
	[Register_Language] [varchar](50) NULL,
	[Student_Username] [varchar](50) NULL,
	[Student_Password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Student_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Takes]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Takes](
	[Student_ID] [varchar](50) NOT NULL,
	[Course_ID] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Course_ID] ASC,
	[Student_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[Teacher_ID] [varchar](50) NOT NULL,
	[Teacher_Name] [varchar](50) NULL,
	[Teacher_Phone] [varchar](50) NULL,
	[Teacher_Email] [varchar](50) NULL,
	[Qualification_Language] [varchar](50) NULL,
	[Teacher_Username] [varchar](50) NULL,
	[Teacher_Password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Teacher_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teaches]    Script Date: 03/31/2025 08:50:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teaches](
	[Teacher_ID] [varchar](50) NOT NULL,
	[Course_ID] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Teacher_ID] ASC,
	[Course_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Assignment] ([Assignment_Code], [Description]) VALUES (N'BT1', N'Assignment')
INSERT [dbo].[Assignment] ([Assignment_Code], [Description]) VALUES (N'BT2', N'Assignment')
INSERT [dbo].[Assignment] ([Assignment_Code], [Description]) VALUES (N'BT3', N'Assignment')
INSERT [dbo].[Assignment] ([Assignment_Code], [Description]) VALUES (N'BT4', N'Assignment')
INSERT [dbo].[Assignment] ([Assignment_Code], [Description]) VALUES (N'EX', N'Exam')
GO
INSERT [dbo].[Attendance] ([Course_ID], [Attendance_Sheet_Link]) VALUES (N'CS001', N'https://docs.google.com/spreadsheets/d/146B-nB9qq5ioMBMN7l85ixPszZjzRmV40jkDT-3sir0/edit?gid=0#gid=0')
INSERT [dbo].[Attendance] ([Course_ID], [Attendance_Sheet_Link]) VALUES (N'CS002', N'https://docs.google.com/spreadsheets/d/1pzrMKXNhQK2npH-uoH_2v6vhQ6ljyJ9wXvIyXRFlCcw/edit?gid=0#gid=0')
INSERT [dbo].[Attendance] ([Course_ID], [Attendance_Sheet_Link]) VALUES (N'CS003', N'https://docs.google.com/spreadsheets/d/1i2ZQ_lOgCufiHNIMgqwkmhU-tfCGc55a_F7yTbPfzJ4/edit?gid=0#gid=0')
GO
INSERT [dbo].[Classroom] ([Room_No], [Capacity]) VALUES (N'B0902', 35)
INSERT [dbo].[Classroom] ([Room_No], [Capacity]) VALUES (N'B0909', 45)
INSERT [dbo].[Classroom] ([Room_No], [Capacity]) VALUES (N'C0102', 40)
INSERT [dbo].[Classroom] ([Room_No], [Capacity]) VALUES (N'C0345', 30)
INSERT [dbo].[Classroom] ([Room_No], [Capacity]) VALUES (N'C0703', 35)
INSERT [dbo].[Classroom] ([Room_No], [Capacity]) VALUES (N'D0104', 20)
INSERT [dbo].[Classroom] ([Room_No], [Capacity]) VALUES (N'D0909', 40)
GO
INSERT [dbo].[Course] ([Course_ID], [Course_Name], [Language_Name]) VALUES (N'CS001', N'English1', N'English')
INSERT [dbo].[Course] ([Course_ID], [Course_Name], [Language_Name]) VALUES (N'CS002', N'French1', N'French')
INSERT [dbo].[Course] ([Course_ID], [Course_Name], [Language_Name]) VALUES (N'CS003', N'English2', N'English')
INSERT [dbo].[Course] ([Course_ID], [Course_Name], [Language_Name]) VALUES (N'CS004', N'Japanese1', N'Japanese')
INSERT [dbo].[Course] ([Course_ID], [Course_Name], [Language_Name]) VALUES (N'CS005', N'English3', N'English')
INSERT [dbo].[Course] ([Course_ID], [Course_Name], [Language_Name]) VALUES (N'CS006', N'French2', N'French')
INSERT [dbo].[Course] ([Course_ID], [Course_Name], [Language_Name]) VALUES (N'CS007', N'French3', N'French')
INSERT [dbo].[Course] ([Course_ID], [Course_Name], [Language_Name]) VALUES (N'CS008', N'Japanese2', N'Japanese')
INSERT [dbo].[Course] ([Course_ID], [Course_Name], [Language_Name]) VALUES (N'CS009', N'Japanese3', N'Japanese')
GO
INSERT [dbo].[Course_Schedule] ([Course_ID], [Starting_Date], [Ending_Date], [Room_No]) VALUES (N'CS001', CAST(N'2025-06-04' AS Date), CAST(N'2025-06-27' AS Date), N'B0902')
INSERT [dbo].[Course_Schedule] ([Course_ID], [Starting_Date], [Ending_Date], [Room_No]) VALUES (N'CS002', CAST(N'2025-03-03' AS Date), CAST(N'2025-03-30' AS Date), N'D0909')
INSERT [dbo].[Course_Schedule] ([Course_ID], [Starting_Date], [Ending_Date], [Room_No]) VALUES (N'CS003', CAST(N'2025-07-12' AS Date), CAST(N'2025-08-03' AS Date), N'B0902')
INSERT [dbo].[Course_Schedule] ([Course_ID], [Starting_Date], [Ending_Date], [Room_No]) VALUES (N'CS004', CAST(N'2025-06-01' AS Date), CAST(N'2025-06-30' AS Date), N'C0345')
INSERT [dbo].[Course_Schedule] ([Course_ID], [Starting_Date], [Ending_Date], [Room_No]) VALUES (N'CS005', CAST(N'2025-08-10' AS Date), CAST(N'2025-09-03' AS Date), N'B0902')
INSERT [dbo].[Course_Schedule] ([Course_ID], [Starting_Date], [Ending_Date], [Room_No]) VALUES (N'CS006', CAST(N'2025-04-10' AS Date), CAST(N'2025-05-05' AS Date), N'D0909')
INSERT [dbo].[Course_Schedule] ([Course_ID], [Starting_Date], [Ending_Date], [Room_No]) VALUES (N'CS007', CAST(N'2025-05-15' AS Date), CAST(N'2025-06-21' AS Date), N'D0909')
INSERT [dbo].[Course_Schedule] ([Course_ID], [Starting_Date], [Ending_Date], [Room_No]) VALUES (N'CS008', CAST(N'2025-07-10' AS Date), CAST(N'2025-08-19' AS Date), N'C0345')
INSERT [dbo].[Course_Schedule] ([Course_ID], [Starting_Date], [Ending_Date], [Room_No]) VALUES (N'CS009', CAST(N'2025-09-01' AS Date), CAST(N'2025-09-24' AS Date), N'C0345')
GO
INSERT [dbo].[Grades] ([Assignment_Code], [Student_ID], [Course_ID], [Assignment_Date], [Assignment_Grade]) VALUES (N'BT1', N'STD001', N'CS001', CAST(N'2025-10-25' AS Date), CAST(7 AS Decimal(18, 0)))
INSERT [dbo].[Grades] ([Assignment_Code], [Student_ID], [Course_ID], [Assignment_Date], [Assignment_Grade]) VALUES (N'BT2', N'STD001', N'CS001', CAST(N'2025-09-20' AS Date), CAST(8 AS Decimal(18, 0)))
INSERT [dbo].[Grades] ([Assignment_Code], [Student_ID], [Course_ID], [Assignment_Date], [Assignment_Grade]) VALUES (N'EX', N'STD001', N'CS001', CAST(N'2025-11-10' AS Date), CAST(6 AS Decimal(18, 0)))
INSERT [dbo].[Grades] ([Assignment_Code], [Student_ID], [Course_ID], [Assignment_Date], [Assignment_Grade]) VALUES (N'BT1', N'STD002', N'CS001', CAST(N'2025-10-25' AS Date), CAST(8 AS Decimal(18, 0)))
INSERT [dbo].[Grades] ([Assignment_Code], [Student_ID], [Course_ID], [Assignment_Date], [Assignment_Grade]) VALUES (N'BT2', N'STD002', N'CS001', CAST(N'2025-09-20' AS Date), CAST(7 AS Decimal(18, 0)))
INSERT [dbo].[Grades] ([Assignment_Code], [Student_ID], [Course_ID], [Assignment_Date], [Assignment_Grade]) VALUES (N'EX', N'STD002', N'CS001', CAST(N'2025-11-10' AS Date), CAST(8 AS Decimal(18, 0)))
GO
INSERT [dbo].[Language] ([Language_Name], [Description]) VALUES (N'English', N'Comprised of Reading, Writing and Listening skill')
INSERT [dbo].[Language] ([Language_Name], [Description]) VALUES (N'French', N'Comprised of Reading, Writing and Listening skill')
INSERT [dbo].[Language] ([Language_Name], [Description]) VALUES (N'German', N'For use in Germany')
INSERT [dbo].[Language] ([Language_Name], [Description]) VALUES (N'Japanese', N'Comprised of Reading, Writing and Listening skill')
INSERT [dbo].[Language] ([Language_Name], [Description]) VALUES (N'Korean', N'Comprised of Reading, Writing and Listening skill')
INSERT [dbo].[Language] ([Language_Name], [Description]) VALUES (N'Mandarin', N'For use in China')
GO
INSERT [dbo].[Prerequisites] ([Course_ID], [Preq_ID]) VALUES (N'CS003', N'CS001')
INSERT [dbo].[Prerequisites] ([Course_ID], [Preq_ID]) VALUES (N'CS005', N'CS003')
INSERT [dbo].[Prerequisites] ([Course_ID], [Preq_ID]) VALUES (N'CS006', N'CS002')
INSERT [dbo].[Prerequisites] ([Course_ID], [Preq_ID]) VALUES (N'CS007', N'CS006')
INSERT [dbo].[Prerequisites] ([Course_ID], [Preq_ID]) VALUES (N'CS008', N'CS004')
INSERT [dbo].[Prerequisites] ([Course_ID], [Preq_ID]) VALUES (N'CS009', N'CS008')
GO
INSERT [dbo].[Protector] ([Student_ID], [Protector_Name], [Protector_Phone]) VALUES (N'STD001', N'Isabella Miranda', N'0939503967')
INSERT [dbo].[Protector] ([Student_ID], [Protector_Name], [Protector_Phone]) VALUES (N'STD002', N'Fran Stiff', N'0928574495')
INSERT [dbo].[Protector] ([Student_ID], [Protector_Name], [Protector_Phone]) VALUES (N'STD003', N'Isadora Gillanders', N'0959115022')
GO
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS001', N'Friday', N'07:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS001', N'Wednesday', N'09:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS002', N'Monday', N'07:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS002', N'Sunday', N'07:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS003', N'Saturday ', N'09:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS003', N'Sunday', N'09:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS004', N'Monday', N'09:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS004', N'Sunday', N'09:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS005', N'Sunday', N'07:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS005', N'Wednesday', N'07:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS006', N'Monday', N'07:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS006', N'Thursday', N'07:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS007', N'Saturday', N'09:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS007', N'Thursday', N'09:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS008', N'Thursday', N'07:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS008', N'Tuesday', N'07:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS009', N'Monday', N'09:30')
INSERT [dbo].[Schedule] ([Course_ID], [DayofWeek], [Starting_Time]) VALUES (N'CS009', N'Wednesday', N'09:30')
GO
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Friday', N'07:30', N'09:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Friday', N'09:30', N'12:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Monday', N'07:30', N'09:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Monday', N'09:30', N'12:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Saturday ', N'07:30', N'09:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Saturday ', N'09:30', N'12:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Sunday', N'07:30', N'09:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Sunday', N'09:30', N'12:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Thursday', N'07:30', N'10:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Thursday', N'09:30', N'12:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Tuesday', N'07:30', N'09:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Tuesday', N'09:30', N'12:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Wednesday', N'07:30', N'09:30')
INSERT [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time], [Ending_Time]) VALUES (N'Wednesday', N'09:30', N'12:30')
GO
INSERT [dbo].[Student] ([Student_ID], [Student_Name], [Student_Phone], [Student_Email], [Student_DOB], [Register_Language], [Student_Username], [Student_Password]) VALUES (N'STD001', N'Neddie Spoor', N'0981008924', N'nhtetien@gmail.com', CAST(N'2003-11-10' AS Date), N'English', N'ntt', N'12345')
INSERT [dbo].[Student] ([Student_ID], [Student_Name], [Student_Phone], [Student_Email], [Student_DOB], [Register_Language], [Student_Username], [Student_Password]) VALUES (N'STD002', N'Josie Fulford', N'0909285245', N'ohshuu@gmail.com', CAST(N'2001-04-20' AS Date), N'English', N'osh', N'12345')
INSERT [dbo].[Student] ([Student_ID], [Student_Name], [Student_Phone], [Student_Email], [Student_DOB], [Register_Language], [Student_Username], [Student_Password]) VALUES (N'STD003', N'John Smith', N'0992050331', N'phanhonhauyen@gmail.com', CAST(N'2005-11-11' AS Date), N'Korean', N'nu', N'12345')
GO
INSERT [dbo].[Takes] ([Student_ID], [Course_ID]) VALUES (N'STD001', N'CS001')
INSERT [dbo].[Takes] ([Student_ID], [Course_ID]) VALUES (N'STD002', N'CS001')
INSERT [dbo].[Takes] ([Student_ID], [Course_ID]) VALUES (N'STD001', N'CS002')
GO
INSERT [dbo].[Teacher] ([Teacher_ID], [Teacher_Name], [Teacher_Phone], [Teacher_Email], [Qualification_Language], [Teacher_Username], [Teacher_Password]) VALUES (N'TEA001', N'Allistir Raulstone', N'0990444021', N'araulstone0@livejournal.com', N'English', N'tch1', N'12345')
INSERT [dbo].[Teacher] ([Teacher_ID], [Teacher_Name], [Teacher_Phone], [Teacher_Email], [Qualification_Language], [Teacher_Username], [Teacher_Password]) VALUES (N'TEA002', N'Riordan Goldis', N'0910445022', N'rgoldis1@bravesites.com', N'French', N'tch2', N'12345')
INSERT [dbo].[Teacher] ([Teacher_ID], [Teacher_Name], [Teacher_Phone], [Teacher_Email], [Qualification_Language], [Teacher_Username], [Teacher_Password]) VALUES (N'TEA003', N'Aleister Crowley', N'0984860338', N'mastermystery@gmail.com', N'Japanese', N'tch3', N'12345')
GO
INSERT [dbo].[Teaches] ([Teacher_ID], [Course_ID]) VALUES (N'TEA001', N'CS001')
INSERT [dbo].[Teaches] ([Teacher_ID], [Course_ID]) VALUES (N'TEA001', N'CS003')
INSERT [dbo].[Teaches] ([Teacher_ID], [Course_ID]) VALUES (N'TEA001', N'CS005')
INSERT [dbo].[Teaches] ([Teacher_ID], [Course_ID]) VALUES (N'TEA002', N'CS002')
INSERT [dbo].[Teaches] ([Teacher_ID], [Course_ID]) VALUES (N'TEA002', N'CS006')
INSERT [dbo].[Teaches] ([Teacher_ID], [Course_ID]) VALUES (N'TEA002', N'CS007')
INSERT [dbo].[Teaches] ([Teacher_ID], [Course_ID]) VALUES (N'TEA003', N'CS004')
INSERT [dbo].[Teaches] ([Teacher_ID], [Course_ID]) VALUES (N'TEA003', N'CS008')
INSERT [dbo].[Teaches] ([Teacher_ID], [Course_ID]) VALUES (N'TEA003', N'CS009')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Student__03DF1FA870EBA554]    Script Date: 03/31/2025 08:50:06 ******/
ALTER TABLE [dbo].[Student] ADD UNIQUE NONCLUSTERED 
(
	[Student_Phone] ASC,
	[Student_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Student__03DF1FA8A93A569D]    Script Date: 03/31/2025 08:50:06 ******/
ALTER TABLE [dbo].[Student] ADD UNIQUE NONCLUSTERED 
(
	[Student_Phone] ASC,
	[Student_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Teacher__19EE7B0F1129887B]    Script Date: 03/31/2025 08:50:06 ******/
ALTER TABLE [dbo].[Teacher] ADD UNIQUE NONCLUSTERED 
(
	[Teacher_Phone] ASC,
	[Teacher_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Teacher__19EE7B0F3428C6A5]    Script Date: 03/31/2025 08:50:06 ******/
ALTER TABLE [dbo].[Teacher] ADD UNIQUE NONCLUSTERED 
(
	[Teacher_Phone] ASC,
	[Teacher_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD FOREIGN KEY([Course_ID])
REFERENCES [dbo].[Course_Schedule] ([Course_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD FOREIGN KEY([Language_Name])
REFERENCES [dbo].[Language] ([Language_Name])
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD FOREIGN KEY([Language_Name])
REFERENCES [dbo].[Language] ([Language_Name])
GO
ALTER TABLE [dbo].[Course_Schedule]  WITH CHECK ADD FOREIGN KEY([Course_ID])
REFERENCES [dbo].[Course] ([Course_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Course_Schedule]  WITH CHECK ADD FOREIGN KEY([Room_No])
REFERENCES [dbo].[Classroom] ([Room_No])
GO
ALTER TABLE [dbo].[Course_Schedule]  WITH CHECK ADD FOREIGN KEY([Room_No])
REFERENCES [dbo].[Classroom] ([Room_No])
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD FOREIGN KEY([Course_ID], [Student_ID])
REFERENCES [dbo].[Takes] ([Course_ID], [Student_ID])
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD FOREIGN KEY([Course_ID], [Student_ID])
REFERENCES [dbo].[Takes] ([Course_ID], [Student_ID])
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD FOREIGN KEY([Assignment_Code])
REFERENCES [dbo].[Assignment] ([Assignment_Code])
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD FOREIGN KEY([Assignment_Code])
REFERENCES [dbo].[Assignment] ([Assignment_Code])
GO
ALTER TABLE [dbo].[Prerequisites]  WITH CHECK ADD FOREIGN KEY([Course_ID])
REFERENCES [dbo].[Course] ([Course_ID])
GO
ALTER TABLE [dbo].[Prerequisites]  WITH CHECK ADD FOREIGN KEY([Course_ID])
REFERENCES [dbo].[Course] ([Course_ID])
GO
ALTER TABLE [dbo].[Prerequisites]  WITH CHECK ADD FOREIGN KEY([Preq_ID])
REFERENCES [dbo].[Course] ([Course_ID])
GO
ALTER TABLE [dbo].[Prerequisites]  WITH CHECK ADD FOREIGN KEY([Preq_ID])
REFERENCES [dbo].[Course] ([Course_ID])
GO
ALTER TABLE [dbo].[Protector]  WITH CHECK ADD FOREIGN KEY([Student_ID])
REFERENCES [dbo].[Student] ([Student_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([DayofWeek], [Starting_Time])
REFERENCES [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time])
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([DayofWeek], [Starting_Time])
REFERENCES [dbo].[Schedule_Time] ([DayofWeek], [Starting_Time])
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([Course_ID])
REFERENCES [dbo].[Course_Schedule] ([Course_ID])
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD FOREIGN KEY([Course_ID])
REFERENCES [dbo].[Course_Schedule] ([Course_ID])
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([Register_Language])
REFERENCES [dbo].[Language] ([Language_Name])
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([Register_Language])
REFERENCES [dbo].[Language] ([Language_Name])
GO
ALTER TABLE [dbo].[Takes]  WITH CHECK ADD FOREIGN KEY([Course_ID])
REFERENCES [dbo].[Course_Schedule] ([Course_ID])
GO
ALTER TABLE [dbo].[Takes]  WITH CHECK ADD FOREIGN KEY([Course_ID])
REFERENCES [dbo].[Course_Schedule] ([Course_ID])
GO
ALTER TABLE [dbo].[Takes]  WITH CHECK ADD FOREIGN KEY([Student_ID])
REFERENCES [dbo].[Student] ([Student_ID])
GO
ALTER TABLE [dbo].[Takes]  WITH CHECK ADD FOREIGN KEY([Student_ID])
REFERENCES [dbo].[Student] ([Student_ID])
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD FOREIGN KEY([Qualification_Language])
REFERENCES [dbo].[Language] ([Language_Name])
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD FOREIGN KEY([Qualification_Language])
REFERENCES [dbo].[Language] ([Language_Name])
GO
ALTER TABLE [dbo].[Teaches]  WITH CHECK ADD FOREIGN KEY([Course_ID])
REFERENCES [dbo].[Course_Schedule] ([Course_ID])
GO
ALTER TABLE [dbo].[Teaches]  WITH CHECK ADD FOREIGN KEY([Course_ID])
REFERENCES [dbo].[Course_Schedule] ([Course_ID])
GO
ALTER TABLE [dbo].[Teaches]  WITH CHECK ADD FOREIGN KEY([Teacher_ID])
REFERENCES [dbo].[Teacher] ([Teacher_ID])
GO
ALTER TABLE [dbo].[Teaches]  WITH CHECK ADD FOREIGN KEY([Teacher_ID])
REFERENCES [dbo].[Teacher] ([Teacher_ID])
GO
