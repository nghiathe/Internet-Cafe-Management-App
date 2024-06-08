-- Tạo bảng

DROP TABLE MaintainanceDetail;
DROP TABLE Maintainance;
DROP TABLE FoodDetail;
DROP TABLE Food;
DROP TABLE Category;
drop table UsageSession;
DROP table billing;
drop table Computer;
DROP table CPU;
DROP table RAM;
DROP table HDD;
DROP table SSD;
DROP table Monitor;
DROP table Mouse;
DROP table GPU;
DROP table Keyboard;
drop table Zone;
drop table EmployeeShift;
drop table Shift;
drop table Employee;
--
CREATE TABLE Employee (
    EmployeeID TINYINT IDENTITY PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    usr NVARCHAR(50),
    pwd NVARCHAR(50)
);

CREATE TABLE Shift (
    ShiftID TINYINT IDENTITY PRIMARY KEY,
    DayOfWeek TINYINT,
    ShiftType TINYINT,
    StartTime TIME,
    EndTime TIME
);

CREATE TABLE EmployeeShift (
    EmployeeID TINYINT,
    ShiftID TINYINT,
    PRIMARY KEY (EmployeeID, ShiftID),
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    FOREIGN KEY (ShiftID) REFERENCES Shift(ShiftID)
);


CREATE TABLE Zone (
    ZoneID TINYINT IDENTITY PRIMARY KEY,
    ZoneName NVARCHAR(50),
    PricePerHour DECIMAL(10, 2)
);

CREATE TABLE CPU (
    CPUID TINYINT IDENTITY PRIMARY KEY,
    CPUModel NVARCHAR(100),
);

CREATE TABLE RAM (
    RAMID TINYINT IDENTITY PRIMARY KEY,
    RAMModel NVARCHAR(Max),
);
GO

CREATE TABLE GPU (
    GPUID TINYINT IDENTITY PRIMARY KEY,
    GPUModel NVARCHAR(100),
);
GO

CREATE TABLE SSD (
    SSDID TINYINT IDENTITY PRIMARY KEY,
    SSDModel NVARCHAR(100),
);
GO

CREATE TABLE HDD (
    HDDID TINYINT IDENTITY PRIMARY KEY,
    HDDModel NVARCHAR(100),
);
GO

CREATE TABLE Monitor (
    MonitorID TINYINT IDENTITY PRIMARY KEY,
    MonitorModel NVARCHAR(50),
    Size TINYINT,
    PixelType NVARCHAR(20),
    RefreshRate INT,
    Resolution NVARCHAR(50)
);
GO

CREATE TABLE Keyboard (
    KeyboardID TINYINT IDENTITY PRIMARY KEY,
    KeyboardModel NVARCHAR(100),
);
GO

CREATE TABLE Mouse (
    MouseID TINYINT IDENTITY PRIMARY KEY,
    MouseModel NVARCHAR(100),
);
GO

CREATE TABLE Computer (
    ComputerID TINYINT IDENTITY PRIMARY KEY,
    ZoneID TINYINT,
    ComputerName NVARCHAR(50),
    ComputerStatus TINYINT,
    CPUID TINYINT,
    GPUID TINYINT,
    RAMID TINYINT,
    SSDID TINYINT,
    HDDID TINYINT,
    MonitorID TINYINT,
    KeyboardID TINYINT,
    MouseID TINYINT,
    CHECK(ComputerStatus in (0,1,2)), -- 0: offline, 1:online 2: bao tri
    FOREIGN KEY (ZoneID) REFERENCES Zone(ZoneID),
    FOREIGN KEY (CPUID) REFERENCES CPU(CPUID),
    FOREIGN KEY (GPUID) REFERENCES GPU(GPUID),
    FOREIGN KEY (SSDID) REFERENCES SSD(SSDID),
    FOREIGN KEY (HDDID) REFERENCES HDD(HDDID),
    FOREIGN KEY (MonitorID) REFERENCES Monitor(MonitorID),
    FOREIGN KEY (KeyboardID) REFERENCES Keyboard(KeyboardID),
    FOREIGN KEY (MouseID) REFERENCES Mouse(MouseID),
);
GO

CREATE TABLE Billing (
	 BillingID INT IDENTITY PRIMARY KEY,
	 BillingType TINYINT,
     BillingDate DATETIME,
     EmployeeID TINYINT,
     Amount DECIMAL(10,2),
     CHECK(BillingType in (0,1)) -- 1: income, -1: expense
);
GO

CREATE TABLE UsageSession (
    SessionID INT IDENTITY PRIMARY KEY,
    ComputerID TINYINT,
    StartTime DATETIME,
    EndTime DATETIME,
    Duration DECIMAL(18,2),
    BillingID INT,
    Cost DECIMAL(18,2),
    FOREIGN KEY (ComputerID) REFERENCES Computer(ComputerID),
    FOREIGN KEY (BillingID) REFERENCES Billing(BillingID)
);
GO

Create table Category (
    CategoryID INT IDENTITY PRIMARY KEY,
    CategoryName NVARCHAR(100)
)

Create table Food (
    FoodID TINYINT IDENTITY PRIMARY KEY,
    FoodName NVARCHAR(100),
    Price DECIMAL(18,2),
    IntakePrice DECIMAL(18,2),
    Inventory INT,
    CategoryID INT,
    Image VARBINARY(MAX),
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);
GO

Create table FoodDetail ( --sử dụng cho cả khách mua hàng, và nhập hàng vào
    BillingID INT,
    FoodID TINYINT,
    Count INT,
    Cost DECIMAL(18,2),
    PRIMARY KEY (BillingID, FoodID),
    FOREIGN KEY (BillingID) REFERENCES Billing(BillingID)
);
GO

CREATE TABLE Maintainance (
	MaintainanceID INT IDENTITY PRIMARY KEY,
	MaintainanceStatus TINYINT, 
	ComputerID TINYINT,
    BillingID INT,
    Cost DECIMAL(18,2),
	FOREIGN KEY (ComputerID) REFERENCES Computer(ComputerID),
    FOREIGN KEY (BillingID) REFERENCES Billing(BillingID)
);
GO

CREATE TABLE MaintainanceDetail (
	MaintainanceID INT,
	ComponentName NVARCHAR(50),
	Description NVARCHAR(100),
	PRIMARY KEY(MaintainanceID, ComponentName),
	CHECK (UPPER(ComponentName) in ('CPU','RAM','VGA','SSD','HDD',N'MÀN HÌNH',N'BÀN PHÍM',N'TAI NGHE',N'CHUỘT',N'PHẦN MỀM',N'KHÁC',N'GHẾ')),
    FOREIGN KEY (MaintainanceID) REFERENCES Maintainance(MaintainanceID)
);
GO

--Insert dữ liệu (trừ các bảng Billing, FoodDetail, DrinkDetail, Maintainance, MaintainanceDetail)

--nhân viên
INSERT INTO Employee (FirstName, LastName, Email, PhoneNumber, usr, pwd) VALUES
('Nguyen', 'Duy Hung', 'hung.nguyen@example.com', '0123456789', 'hungduy123', 'hungduy123'),
('Nguyen', 'The Nghia', 'nghia.the@example.com', '0987654321', 'nghiathe123', 'nghiathe123'),
('Dinh', 'Thanh Nam', 'nam.dinh@example.com', '0382027003', 'namthanh123', 'namthanh123');
-- Thêm ca làm việc
INSERT INTO Shift(DayOfWeek, ShiftType, StartTime, EndTime) VALUES
(1, 1, '06:00:00', '14:00:00'), -- Thứ Hai, ca sáng
(1, 2, '14:00:00', '22:00:00'), -- Thứ Hai, ca chiều
(1, 3, '22:00:00', '06:00:00'), -- Thứ Hai, ca tối
(2, 1, '06:00:00', '14:00:00'), -- Thứ Ba
(2, 2, '14:00:00', '22:00:00'), -- Thứ Ba
(2, 3, '22:00:00', '06:00:00'), -- Thứ Ba
(3, 1, '06:00:00', '14:00:00'), -- Thứ Tư
(3, 2, '14:00:00', '22:00:00'), -- Thứ Tư
(3, 3, '22:00:00', '06:00:00'), -- Thứ Tư
(4, 1, '06:00:00', '14:00:00'), -- Thứ Năm
(4, 2, '14:00:00', '22:00:00'), -- Thứ Năm
(4, 3, '22:00:00', '06:00:00'), -- Thứ Năm
(5, 1, '06:00:00', '14:00:00'), -- Thứ Sáu, ca sáng
(5, 2, '14:00:00', '22:00:00'), -- Thứ Sáu, ca chiều
(5, 3, '22:00:00', '06:00:00'), -- Thứ Sáu, ca tối
(6, 1, '06:00:00', '14:00:00'), -- Thứ Bảy, ca sáng
(6, 2, '14:00:00', '22:00:00'), -- Thứ Bảy, ca chiều
(6, 3, '22:00:00', '06:00:00'), -- Thứ Bảy, ca tối
(0, 1, '06:00:00', '14:00:00'), -- CN, ca sáng
(0, 2, '14:00:00', '22:00:00'), -- CN, ca chiều
(0, 3, '22:00:00', '06:00:00'); -- CN, ca tối

-- Phân công ca làm việc cho nhân viên
INSERT INTO EmployeeShift (EmployeeID, ShiftID) VALUES
(1, 1), -- Nguyen Duy hung lam sang
(2, 2), -- Nguyen The Nghia lam chieu
(3, 3), -- Dinh Thanh Nam lam toi
(1, 4), -- Nguyen Duy hung lam sang
(2, 5), -- Nguyen The Nghia lam chieu
(3, 6), -- Dinh Thanh Nam lam toi
(1, 7), -- Nguyen Duy hung lam sang
(2, 8), -- Nguyen The Nghia lam chieu
(3, 9), -- Dinh Thanh Nam lam toi
(1, 10), -- Nguyen Duy hung lam sang
(2, 11), -- Nguyen The Nghia lam chieu
(3, 12), -- Dinh Thanh Nam lam toi
(1, 13), -- Nguyen Duy hung lam sang
(2, 14), -- Nguyen The Nghia lam chieu
(3, 15), -- Dinh Thanh Nam lam toi
(1, 16), -- Nguyen Duy hung lam sang
(2, 17), -- Nguyen The Nghia lam chieu
(3, 18), -- Dinh Thanh Nam lam toi
(1, 19), -- Nguyen Duy hung lam sang
(2, 20), -- Nguyen The Nghia lam chieu
(3, 21); -- Dinh Thanh Nam lam toi

INSERT INTO Zone
(ZoneName, PricePerHour)
VALUES
(N'Tiêu chuẩn', 7000),
(N'Gaming', 9000),
(N'Chuyên nghiệp', 14000),
(N'Thi đấu', 15000)
GO

INSERT INTO CPU
(CPUModel)
VALUES
(N'Intel Core i3-12100F'),
(N'Intel Core i5-12400F'),
(N'Intel Core i7-12700K'),
(N'Intel Core i7-12700KF')
GO

INSERT INTO RAM
(RAMModel)
VALUES
(N'Corsair Vengeance LPX DDR4 3200 C16 2x8GB'),
(N'Corsair Vengeance LPX DDR4 3200 C16 2x8GB'),
(N'Corsair Vengeance LPX DDR4 3600 C18 2x16GB'),
(N'Corsair Vengeance LPX DDR4 3600 C18 2x16GB')
GO

INSERT INTO GPU
(GPUModel)
VALUES
(N'Nvidia GTX 1650S (Super)'),
(N'Nvidia GTX 1660S (Super)'),
(N'Nvidia RTX 3060-Ti'),
(N'Nvidia RTX 4060-Ti')
GO

INSERT INTO SSD
(SSDModel)
VALUES
(N'Samsung 980 Pro NVMe PCIe M.2 500GB'),
(N'Samsung 980 Pro NVMe PCIe M.2 500GB'),
(N'Samsung 980 Pro NVMe PCIe M.2 1TB'),
(N'Samsung 980 Pro NVMe PCIe M.2 1TB')
GO

INSERT INTO HDD
(HDDModel)
VALUES
(N'WD Black 2TB'),
(N'WD Black 2TB'),
(N'WD Black 2TB'),
(N'WD Black 2TB')
GO


INSERT INTO Monitor
(MonitorModel, Size, PixelType, RefreshRate, Resolution)
VALUES
(N'ASUS TUF Gaming VG249Q1A', 24, 'IPS', 144,'1920 x 1080'),
(N'LG 27GS95QE-B', 27, 'IPS', 165,'2560 x 1440'),
(N'Dell Alienware AW2725DF', 27, 'QD-OLED', 165,'2560 x 1440'),
(N'Acer Nitro XV275K P3biipruzx', 27, 'IPS', 160,'3840 x 2160')
GO

INSERT INTO Keyboard
(KeyboardModel)
VALUES
(N'Bàn phím Redragon S101'),
(N'Bàn phím Corsair K55'),
(N'Bàn phím Razer BlackWidow Elite'),
(N'Bàn phím Logitech G915 TKL')
GO

INSERT INTO Mouse
(MouseModel)
VALUES
(N'Chuột Redragon S101'),
(N'Chuột Corsair Harpoon'),
(N'Chuột Razer DeathAdder Elite'),
(N'Chuột Logitech G Pro Wireless')
GO

INSERT INTO Computer
(ComputerName, ComputerStatus, ZoneID, CPUID, GPUID, RAMID, SSDID, HDDID, MonitorID, KeyboardID, MouseID)
VALUES
(N'Máy 01', 0, 1, 1, 1, 1, 1, 1, 1, 1, 1),
(N'Máy 02', 0, 1, 1, 1, 1, 1, 1, 1, 1, 1),
(N'Máy 03', 0, 1, 1, 1, 1, 1, 1, 1, 1, 1),
(N'Máy 04', 0, 1, 1, 1, 1, 1, 1, 1, 1, 1),
(N'Máy 05', 0, 1, 1, 1, 1, 1, 1, 1, 1, 1),
(N'Máy 06', 0, 1, 1, 1, 1, 1, 1, 1, 1, 1),
(N'Máy 07', 0, 1, 1, 1, 1, 1, 1, 1, 1, 1),
(N'Máy 08', 0, 1, 1, 1, 1, 1, 1, 1, 1, 1),
(N'Máy 09', 0, 1, 1, 1, 1, 1, 1, 1, 1, 1),
(N'Máy 10', 0, 1, 1, 1, 1, 1, 1, 1, 1, 1),
(N'Máy 11', 0, 2, 2, 2, 2, 2, 2, 2, 2, 2),
(N'Máy 12', 0, 2, 2, 2, 2, 2, 2, 2, 2, 2),
(N'Máy 13', 0, 2, 2, 2, 2, 2, 2, 2, 2, 2),
(N'Máy 14', 0, 2, 2, 2, 2, 2, 2, 2, 2, 2),
(N'Máy 15', 0, 2, 2, 2, 2, 2, 2, 2, 2, 2),
(N'Máy 16', 0, 2, 2, 2, 2, 2, 2, 2, 2, 2),
(N'Máy 17', 0, 2, 2, 2, 2, 2, 2, 2, 2, 2),
(N'Máy 18', 0, 2, 2, 2, 2, 2, 2, 2, 2, 2),
(N'Máy 19', 0, 2, 2, 2, 2, 2, 2, 2, 2, 2),
(N'Máy 20', 0, 2, 2, 2, 2, 2, 2, 2, 2, 2),
(N'Máy 21', 0, 3, 3, 3, 3, 3, 3, 3, 3, 3),
(N'Máy 22', 0, 3, 3, 3, 3, 3, 3, 3, 3, 3),
(N'Máy 23', 0, 3, 3, 3, 3, 3, 3, 3, 3, 3),
(N'Máy 24', 0, 3, 3, 3, 3, 3, 3, 3, 3, 3),
(N'Máy 25', 0, 3, 3, 3, 3, 3, 3, 3, 3, 3),
(N'Máy 26', 0, 3, 3, 3, 3, 3, 3, 3, 3, 3),
(N'Máy 27', 0, 3, 3, 3, 3, 3, 3, 3, 3, 3),
(N'Máy 28', 0, 3, 3, 3, 3, 3, 3, 3, 3, 3),
(N'Máy 29', 0, 3, 3, 3, 3, 3, 3, 3, 3, 3),
(N'Máy 30', 0, 3, 3, 3, 3, 3, 3, 3, 3, 3),
(N'Máy 31', 0, 4, 4, 4, 4, 4, 4, 4, 4, 4),
(N'Máy 32', 0, 4, 4, 4, 4, 4, 4, 4, 4, 4),
(N'Máy 33', 0, 4, 4, 4, 4, 4, 4, 4, 4, 4),
(N'Máy 34', 0, 4, 4, 4, 4, 4, 4, 4, 4, 4),
(N'Máy 35', 0, 4, 4, 4, 4, 4, 4, 4, 4, 4),
(N'Máy 36', 0, 4, 4, 4, 4, 4, 4, 4, 4, 4),
(N'Máy 37', 0, 4, 4, 4, 4, 4, 4, 4, 4, 4),
(N'Máy 38', 0, 4, 4, 4, 4, 4, 4, 4, 4, 4),
(N'Máy 39', 0, 4, 4, 4, 4, 4, 4, 4, 4, 4),
(N'Máy 40', 0, 4, 4, 4, 4, 4, 4, 4, 4, 4);
GO

INSERT into Category(CategoryName)
VALUES
(N'Cơm'),
(N'Phở & bún & mì'),
(N'Bánh mì'),
(N'Đồ ăn nhẹ'),
(N'Đồ uống'),
(N'Kem');

-- Các view thông dụng
--
-- IF EXISTS (
--     SELECT 1
--     FROM sys.views where name = N'ViewMaintainanceInfo'
-- )
-- DROP VIEW ViewMaintainanceInfo;
-- GO

-- CREATE VIEW ViewMaintainanceInfo
-- AS
--     SELECT 
--         md.MaintainanceID,
--         m.MaintainanceStatus,
--         m.ComputerID as MaintainedComputer,
--         m.BillingID,
--         md.ComponentName as MaintainedComponent,
--         md.Description,
--         m.Cost as TotalMaintainingFee
--     FROM MaintainanceDetail md JOIN Maintainance m ON md.MaintainanceID = m.MaintainanceID
-- GO


-- Các Proc thông dụng



-- Thủ tục khởi tạo Billing: 1 tham số duy nhất: BillingType ={0, 1}
DROP PROC ProcBillingINIT; 
GO
--
CREATE PROCEDURE ProcBillingINIT
    @Billingtype TINYINT
AS
BEGIN
    INSERT INTO Billing (BillingType)
    VALUES (@Billingtype);
END
GO
-- Thủ tục khởi tạo UsageSession: 1 tham số duy nhất: ComputerID = {1,...,40}
DROP PROCEDURE ProcUsageSessionINIT; 
GO
--
CREATE PROCEDURE ProcUsageSessionINIT
    @ComputerID TINYINT
AS
BEGIN
    DECLARE @p_BillingID INT = (SELECT TOP 1 BillingID 
                                FROM Billing 
                                ORDER BY BillingID DESC);
    -- Thêm phiên sử dụng mới
    INSERT INTO UsageSession (BillingID, ComputerID, StartTime) 
    VALUES (@p_BillingID, @ComputerID, GETDATE());
    EXEC ProcComputerStatus @ComputerID, 1;
END
GO

-- Thủ tục khởi tạo FoodDetail
DROP PROCEDURE ProcFoodDetailINIT
GO

CREATE PROCEDURE ProcFoodDetailINIT
    @BillingID INT,
    @FoodID INT,
    @Count INT
AS
BEGIN
    INSERT INTO FoodDetail (BillingID, FoodID, Count) 
    VALUES (@BillingID,@FoodID, @Count)
END
GO

Drop PROCEDURE ProcMaintainanceINIT;
GO

-- Thủ tục khởi tạo Maintainance
CREATE PROCEDURE ProcMaintainanceINIT
    @ComputerID TINYINT
AS
BEGIN
    DECLARE @billingID INT = (SELECT TOP 1 BillingID 
                                  FROM Billing 
                                  ORDER BY BillingID DESC);

        INSERT INTO Maintainance (ComputerID, BillingID)
        VALUES (@ComputerID, @billingID);
		EXEC ProcComputerStatus @ComputerID, 2;
END;
GO

DROP PROC ProcComputerStatus; 
GO

CREATE PROC ProcComputerStatus
    @ComputerID TINYINT,
    @ComputerStatus TINYINT
AS
BEGIN
    UPDATE c
    SET c.ComputerStatus = @ComputerStatus
    FROM Computer c
    WHERE c.ComputerID = @ComputerID;
END
GO

--Trigger cập nhật Cost mỗi khi có Fooddetail được khởi tạo hoặc được cập nhật Count

IF EXISTS (
    SELECT 1
    FROM sys.triggers where name = N'TrigFoodDetailAfterIU'
)
DROP TRIGGER TrigFoodDetailAfterIU; 
GO

CREATE TRIGGER TrigFoodDetailAfterIU
ON FoodDetail
AFTER INSERT, UPDATE
AS
BEGIN
    -- Cập nhật giá trị Cost dựa trên Count và Price từ bảng Food
    UPDATE fd
    SET fd.Cost = i.Count * f.Price
    FROM FoodDetail fd
    INNER JOIN inserted i ON fd.BillingID = i.BillingID AND fd.FoodID = i.FoodID
    INNER JOIN Food f ON fd.FoodID = f.FoodID
    INNER JOIN Billing b on b.BillingID = i.BillingID
    WHERE BillingType = 1;

    -- Cập nhật hàng tồn kho sau khi thêm mới hoặc cập nhật trong FoodDetail
    UPDATE f
    SET f.Inventory = f.Inventory + isnull(d.Count, 0) - i.Count
    FROM Food f
    LEFT JOIN deleted d ON f.FoodID = d.FoodID
    LEFT JOIN inserted i ON f.FoodID = i.FoodID
    LEFT JOIN Billing b ON i.BillingID = b.BillingID
    WHERE b.BillingType = 1;

    -- Cập nhật giá trị Cost dựa trên Count và IntakePrice từ bảng Food
    UPDATE fd
    SET fd.Cost = i.Count * f.IntakePrice
    FROM FoodDetail fd
    INNER JOIN inserted i ON fd.BillingID = i.BillingID AND fd.FoodID = i.FoodID
    INNER JOIN Food f ON fd.FoodID = f.FoodID
    INNER JOIN Billing b on b.BillingID = i.BillingID
    WHERE BillingType = 0;

    -- Cập nhật hàng tồn kho sau khi nhập hàng trong FoodDetail
    UPDATE f
    SET f.Inventory = f.Inventory + i.Count
    FROM Food f
    INNER JOIN inserted i ON f.FoodID = i.FoodID
    INNER JOIN Billing b ON i.BillingID = b.BillingID
    WHERE b.BillingType = 0;
    
END;
GO

--
Drop FUNCTION FuncDurationCal
GO

CREATE FUNCTION FuncDurationCal (
    @StartTime DATETIME,
    @EndTime DATETIME
)
RETURNS INT
AS
BEGIN
    DECLARE @DurationMinutes INT;
    SET @DurationMinutes = DATEDIFF(MINUTE, @StartTime, @EndTime);
    RETURN @DurationMinutes;
END;
GO
--
DROP PROCEDURE IF EXISTS ProcEndSession;
GO

CREATE PROCEDURE ProcEndSession
    @BillingID INT
AS
BEGIN
    -- Cập nhật EndTime, Duration và tính toán Cost cho UsageSession
    DECLARE @currentDateTime DATETIME;
    SET @currentDateTime = GETDATE();

    UPDATE u
    SET 
        u.EndTime = @currentDateTime,
        u.Duration = dbo.FuncDurationCal(u.StartTime, @currentDateTime), -- Cập nhật Duration
        u.Cost = CAST(COALESCE(u.Duration / 60.0 * z.PricePerHour, 0) AS DECIMAL(18, 2)) -- Tính toán Cost
    FROM 
        UsageSession u 
        LEFT JOIN Computer c ON c.ComputerID = u.ComputerID
        LEFT JOIN Zone z ON z.ZoneID = c.ZoneID
    WHERE 
        u.BillingID = @BillingID;
END;
GO

-- Thủ tục checkout và tính tiền full + cập nhật trạng thái PC
--BillingID identity INT vô hạn, EmployeeID tinyint < 255
--2 tham số : @BillingID và @EmployeeID
DROP PROC ProcCheckOut
GO

CREATE PROC ProcCheckOut
    @BillingID INT,
    @EmployeeID TINYINT
AS
BEGIN
    DECLARE @computerID TINYINT = (SELECT u.ComputerID FROM 
                                UsageSession u 
                                WHERE u.BillingID = @BillingID);
    IF EXISTS (
    SELECT *
    FROM UsageSession
    WHERE BillingID =@BillingID
    AND ComputerID = @computerID
    )
    EXEC ProcEndSession @BillingID;

    -- Cập nhật thông tin Billing
    UPDATE b
    SET 
        b.EmployeeID = @EmployeeID,
        b.BillingDate = GETDATE(),
        b.Amount = (ISNULL(fd.SumCost, 0) + 
                    ISNULL(u.Cost, 0)) +
                    ISNULL(m.Cost, 0)
    FROM 
        Billing b
        LEFT JOIN 
        (
            SELECT BillingID, SUM(Cost) AS SumCost
            FROM FoodDetail
            GROUP BY BillingID
        ) fd ON b.BillingID = fd.BillingID
        LEFT JOIN 
        (
            SELECT BillingID, Cost
            FROM UsageSession
        ) u ON b.BillingID = u.BillingID
        LEFT JOIN
        (
            SELECT BillingID, Cost
            FROM Maintainance
        ) m on b.BillingID = m.BillingID
    WHERE 
        b.BillingID = @BillingID;
    EXECUTE ProcComputerStatus @computerID, 0;
END
GO

DROP PROCEDURE GetUsageSessionDetails;
GO
CREATE PROCEDURE GetUsageSessionDetails
    @ComputerID TINYINT
AS
BEGIN
    SELECT 
        c.ComputerName, 
        c.ComputerStatus,
		CASE 
            WHEN c.ComputerStatus = 0 or c.ComputerStatus = 2 THEN NULL 
            ELSE us.BillingID 
        END AS BillingID,
        CASE 
            WHEN c.ComputerStatus = 0 or c.ComputerStatus = 2 THEN NULL 
            ELSE us.StartTime 
        END AS StartTime
    FROM 
        Computer c
    left JOIN 
		(UsageSession us
			left JOIN 
				Billing b ON us.BillingID = b.BillingID)
    ON c.ComputerID = us.ComputerID  
    WHERE 
        c.ComputerID = @ComputerID
    AND 
        (c.ComputerStatus = 0 OR c.ComputerStatus = 2 OR (c.ComputerStatus = 1 AND b.BillingDate IS NULL))
END
GO

DROP PROCEDURE GetUnCheckOutSession; 
GO
CREATE PROCEDURE GetUnCheckOutSession
    @comid TINYINT
AS
BEGIN
SELECT 
    us.BillingID, 
    c.ComputerName, 
    c.ComputerStatus, 
    us.StartTime
FROM 
    UsageSession us
JOIN 
    Computer c ON us.ComputerID = c.ComputerID
JOIN 
    Billing b ON us.BillingID = b.BillingID
WHERE 
    c.ComputerID = @comid AND
    b.BillingDate IS NULL;
END;
GO

DROP PROCEDURE GetComputerDetailsByZone; 
GO
CREATE PROCEDURE GetComputerDetailsByZone
    @ZoneID TINYINT
AS
BEGIN
    SELECT 
		c.computerid,
        c.ComputerName,
        z.ZoneName,
        c.ComputerStatus,
        z.PricePerHour,
        cpu.CPUModel,
        gpu.GPUModel,
        hdd.HDDModel,
        ssd.SSDModel,
        mouse.MouseModel,
        keyboard.KeyboardModel,
        monitor.MonitorModel
    FROM 
        Computer c
    JOIN 
        Zone z ON c.ZoneID = z.ZoneID
    JOIN 
        CPU cpu ON c.CPUID = cpu.CPUID
    JOIN 
        GPU gpu ON c.GPUID = gpu.GPUID
    JOIN 
        HDD hdd ON c.HDDID = hdd.HDDID
    JOIN 
        SSD ssd ON c.SSDID = ssd.SSDID
    JOIN 
        Mouse mouse ON c.MouseID = mouse.MouseID
    JOIN 
        Keyboard keyboard ON c.KeyboardID = keyboard.KeyboardID
    JOIN 
        Monitor monitor ON c.MonitorID = monitor.MonitorID
    WHERE 
        c.ZoneID = @ZoneID;
END;
GO

DROP PROCEDURE GetFoodDetailsByComputerID; 
GO
CREATE PROCEDURE GetFoodDetailsByComputerID
    @ComputerID TINYINT
AS
BEGIN
    SELECT 
        f.FoodID,
        f.FoodName, 
        f.CategoryID,
        fd.Count, 
        f.Price, 
        fd.Cost,
        f.Image
    FROM 
        UsageSession us
    JOIN 
        Billing b ON us.BillingID = b.BillingID
    JOIN 
        FoodDetail fd ON b.BillingID = fd.BillingID
    JOIN 
        Food f ON fd.FoodID = f.FoodID
    WHERE 
        us.ComputerID = @ComputerID
        AND b.BillingType = 1
        AND b.BillingDate IS NULL;
END;
GO



DROP PROCEDURE GetBillByDate; 
GO
create proc GetBillByDate
@ngaybd datetime, @ngaykt datetime
as
begin
	SELECT 
        b.BillingDate,
        e.FirstName + ' ' + e.LastName AS EmployeeName,
        b.BillingType,
        ISNULL(s.SessionCost, 0) AS SessionCost,
        ISNULL(m.MaintainanceCost, 0) AS MaintainanceCost,
        ISNULL(f.FoodCost, 0) AS FoodCost,
        b.Amount
    FROM 
        Billing b
    LEFT JOIN 
        Employee e ON b.EmployeeID = e.EmployeeID
    LEFT JOIN 
        (SELECT BillingID, SUM(Cost) AS SessionCost FROM UsageSession GROUP BY BillingID) s ON b.BillingID = s.BillingID
    LEFT JOIN 
        (SELECT BillingID, SUM(Cost) AS MaintainanceCost FROM Maintainance GROUP BY BillingID) m ON b.BillingID = m.BillingID
    LEFT JOIN 
        (SELECT BillingID, SUM(Cost) AS FoodCost FROM FoodDetail GROUP BY BillingID) f ON b.BillingID = f.BillingID
	where BillingDate between @ngaybd and @ngaykt
end;
go

Drop PROCEDURE AddMaintainanceDetail;
GO

CREATE PROCEDURE AddMaintainanceDetail
    @MaintainanceID INT,
    @ComponentName NVARCHAR(50),
    @Description NVARCHAR(100) = null
AS
BEGIN
    INSERT INTO MaintainanceDetail (MaintainanceID, ComponentName, Description)
    VALUES (@MaintainanceID, @ComponentName, @Description);
END;
go

Drop PROCEDURE GetUnCheckOutMaintainance;
GO

CREATE PROCEDURE GetUnCheckOutMaintainance
    @ComputerID TINYINT
AS
BEGIN
    SELECT m.MaintainanceID,
		   m.BillingID,
           md.ComponentName, 
           md.Description
    FROM Maintainance m
    JOIN MaintainanceDetail md ON m.MaintainanceID = md.MaintainanceID
    WHERE m.ComputerID = @ComputerID
      AND m.Cost IS NULL;
END;
go
