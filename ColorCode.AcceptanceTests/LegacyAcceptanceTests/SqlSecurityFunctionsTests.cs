﻿using Xunit;

namespace MarkdownServer.ColorCode.SqlAcceptanceTests
{
    public class SqlSecurityFunctionsTests
    {
        public class Transform
        {
            [Fact]
            public void WillStyleIsMemberFunction()
            {
                string sourceText =
@"-- Test membership in db_owner and print appropriate message.
IF IS_MEMBER ('db_owner') = 1
    print 'Current user is a member of the db_owner role'
ELSE IF IS_MEMBER ('db_owner') = 0
    print 'Current user is NOT a member of the db_owner role'
ELSE IF IS_MEMBER ('db_owner') IS NULL
    print 'ERROR: Invalid group / role specified'
go

-- Execute SELECT if user is a member of ADVWORKS\Shipping.
IF IS_MEMBER ('ADVWORKS\Shipping') = 1
    SELECT 'User ' + USER + ' is a member of ADVWORKS\Shipping.'
go";
                string expected =
@"<div style=""color:Black;background-color:White;""><pre>
<span style=""color:Green;"">-- Test membership in db_owner and print appropriate message.</span>
<span style=""color:Blue;"">IF</span> <span style=""color:Blue;"">IS_MEMBER</span> (<span style=""color:#A31515;"">&#39;db_owner&#39;</span>) = 1
    <span style=""color:Blue;"">print</span> <span style=""color:#A31515;"">&#39;Current user is a member of the db_owner role&#39;</span>
<span style=""color:Blue;"">ELSE</span> <span style=""color:Blue;"">IF</span> <span style=""color:Blue;"">IS_MEMBER</span> (<span style=""color:#A31515;"">&#39;db_owner&#39;</span>) = 0
    <span style=""color:Blue;"">print</span> <span style=""color:#A31515;"">&#39;Current user is NOT a member of the db_owner role&#39;</span>
<span style=""color:Blue;"">ELSE</span> <span style=""color:Blue;"">IF</span> <span style=""color:Blue;"">IS_MEMBER</span> (<span style=""color:#A31515;"">&#39;db_owner&#39;</span>) <span style=""color:Blue;"">IS</span> <span style=""color:Blue;"">NULL</span>
    <span style=""color:Blue;"">print</span> <span style=""color:#A31515;"">&#39;ERROR: Invalid group / role specified&#39;</span>
go

<span style=""color:Green;"">-- Execute SELECT if user is a member of ADVWORKS\Shipping.</span>
<span style=""color:Blue;"">IF</span> <span style=""color:Blue;"">IS_MEMBER</span> (<span style=""color:#A31515;"">&#39;ADVWORKS\Shipping&#39;</span>) = 1
    <span style=""color:Blue;"">SELECT</span> <span style=""color:#A31515;"">&#39;User &#39;</span> + <span style=""color:Magenta;"">USER</span> + <span style=""color:#A31515;"">&#39; is a member of ADVWORKS\Shipping.&#39;</span>
go
</pre></div>";

                string actual = new CodeColorizer().Colorize(sourceText, Languages.Sql);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void WillStyleSuserSidFunction()
            {
                string sourceText =
@"USE AdventureWorks;
GO
CREATE TABLE sid_example
(
login_sid   varbinary(85) DEFAULT SUSER_SID(),
login_name  varchar(30) DEFAULT SYSTEM_USER,
login_dept  varchar(10) DEFAULT 'SALES',
login_date  datetime DEFAULT GETDATE()
)
GO
INSERT sid_example DEFAULT VALUES
GO";
                string expected =
@"<div style=""color:Black;background-color:White;""><pre>
<span style=""color:Blue;"">USE</span> AdventureWorks;
GO
<span style=""color:Blue;"">CREATE</span> <span style=""color:Blue;"">TABLE</span> sid_example
(
login_sid   <span style=""color:Blue;"">varbinary</span>(85) <span style=""color:Blue;"">DEFAULT</span> <span style=""color:Blue;"">SUSER_SID</span>(),
login_name  <span style=""color:Blue;"">varchar</span>(30) <span style=""color:Blue;"">DEFAULT</span> <span style=""color:Magenta;"">SYSTEM_USER</span>,
login_dept  <span style=""color:Blue;"">varchar</span>(10) <span style=""color:Blue;"">DEFAULT</span> <span style=""color:#A31515;"">&#39;SALES&#39;</span>,
login_date  <span style=""color:Blue;"">datetime</span> <span style=""color:Blue;"">DEFAULT</span> <span style=""color:Magenta;"">GETDATE</span>()
)
GO
<span style=""color:Blue;"">INSERT</span> sid_example <span style=""color:Blue;"">DEFAULT</span> <span style=""color:Blue;"">VALUES</span>
GO
</pre></div>";

                string actual = new CodeColorizer().Colorize(sourceText, Languages.Sql);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void WillStyleSuserSnameFunction()
            {
                string sourceText =
@"SELECT SUSER_SNAME(0x01);
GO

SELECT SUSER_SNAME(0x010500000000000515000000a065cf7e784b9b5fe77c87705a2e0000);
GO

USE AdventureWorks;
GO
CREATE TABLE sname_example
(
login_sname sysname DEFAULT SUSER_SNAME(),
employee_id uniqueidentifier DEFAULT NEWID(),
login_date  datetime DEFAULT GETDATE()
)
GO
INSERT sname_example DEFAULT VALUES
GO";
                string expected =
@"<div style=""color:Black;background-color:White;""><pre>
<span style=""color:Blue;"">SELECT</span> <span style=""color:Blue;"">SUSER_SNAME</span>(0x01);
GO

<span style=""color:Blue;"">SELECT</span> <span style=""color:Blue;"">SUSER_SNAME</span>(0x010500000000000515000000a065cf7e784b9b5fe77c87705a2e0000);
GO

<span style=""color:Blue;"">USE</span> AdventureWorks;
GO
<span style=""color:Blue;"">CREATE</span> <span style=""color:Blue;"">TABLE</span> sname_example
(
login_sname <span style=""color:Blue;"">sysname</span> <span style=""color:Blue;"">DEFAULT</span> <span style=""color:Blue;"">SUSER_SNAME</span>(),
employee_id <span style=""color:Blue;"">uniqueidentifier</span> <span style=""color:Blue;"">DEFAULT</span> <span style=""color:Magenta;"">NEWID</span>(),
login_date  <span style=""color:Blue;"">datetime</span> <span style=""color:Blue;"">DEFAULT</span> <span style=""color:Magenta;"">GETDATE</span>()
)
GO
<span style=""color:Blue;"">INSERT</span> sname_example <span style=""color:Blue;"">DEFAULT</span> <span style=""color:Blue;"">VALUES</span>
GO
</pre></div>";

                string actual = new CodeColorizer().Colorize(sourceText, Languages.Sql);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void WillStyleIsSrvrolememberFunction()
            {
                string sourceText =
@"IF IS_SRVROLEMEMBER ('sysadmin') = 1
    print 'Current user''s login is a member of the sysadmin role'
ELSE IF IS_SRVROLEMEMBER ('sysadmin') = 0
    print 'Current user''s login is NOT a member of the sysadmin role'
ELSE IF IS_SRVROLEMEMBER ('sysadmin') IS NULL
    print 'ERROR: The server role specified is not valid.'";
                string expected =
@"<div style=""color:Black;background-color:White;""><pre>
<span style=""color:Blue;"">IF</span> <span style=""color:Blue;"">IS_SRVROLEMEMBER</span> (<span style=""color:#A31515;"">&#39;sysadmin&#39;</span>) = 1
    <span style=""color:Blue;"">print</span> <span style=""color:#A31515;"">&#39;Current user&#39;</span><span style=""color:#A31515;"">&#39;s login is a member of the sysadmin role&#39;</span>
<span style=""color:Blue;"">ELSE</span> <span style=""color:Blue;"">IF</span> <span style=""color:Blue;"">IS_SRVROLEMEMBER</span> (<span style=""color:#A31515;"">&#39;sysadmin&#39;</span>) = 0
    <span style=""color:Blue;"">print</span> <span style=""color:#A31515;"">&#39;Current user&#39;</span><span style=""color:#A31515;"">&#39;s login is NOT a member of the sysadmin role&#39;</span>
<span style=""color:Blue;"">ELSE</span> <span style=""color:Blue;"">IF</span> <span style=""color:Blue;"">IS_SRVROLEMEMBER</span> (<span style=""color:#A31515;"">&#39;sysadmin&#39;</span>) <span style=""color:Blue;"">IS</span> <span style=""color:Blue;"">NULL</span>
    <span style=""color:Blue;"">print</span> <span style=""color:#A31515;"">&#39;ERROR: The server role specified is not valid.&#39;</span>
</pre></div>";

                string actual = new CodeColorizer().Colorize(sourceText, Languages.Sql);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void WillStylePermissionsFunction()
            {
                string sourceText =
@"IF PERMISSIONS()&2=2
    CREATE TABLE test_table (col1 INT)
ELSE
    PRINT 'ERROR: The current user cannot create a table.';

IF PERMISSIONS(OBJECT_ID('AdventureWorks.Person.Address','U'))&8=8
    PRINT 'The current user can insert data into Person.Address.'
ELSE
    PRINT 'ERROR: The current user cannot insert data into Person.Address.';

IF PERMISSIONS(OBJECT_ID('AdventureWorks.Person.Address','U'))&0x80000=0x80000
    PRINT 'INSERT on Person.Address is grantable.'
ELSE
    PRINT 'You may not GRANT INSERT permissions on Person.Address.';";
                string expected =
@"<div style=""color:Black;background-color:White;""><pre>
<span style=""color:Blue;"">IF</span> <span style=""color:Blue;"">PERMISSIONS</span>()&amp;2=2
    <span style=""color:Blue;"">CREATE</span> <span style=""color:Blue;"">TABLE</span> test_table (col1 <span style=""color:Blue;"">INT</span>)
<span style=""color:Blue;"">ELSE</span>
    <span style=""color:Blue;"">PRINT</span> <span style=""color:#A31515;"">&#39;ERROR: The current user cannot create a table.&#39;</span>;

<span style=""color:Blue;"">IF</span> <span style=""color:Blue;"">PERMISSIONS</span>(<span style=""color:Blue;"">OBJECT_ID</span>(<span style=""color:#A31515;"">&#39;AdventureWorks.Person.Address&#39;</span>,<span style=""color:#A31515;"">&#39;U&#39;</span>))&amp;8=8
    <span style=""color:Blue;"">PRINT</span> <span style=""color:#A31515;"">&#39;The current user can insert data into Person.Address.&#39;</span>
<span style=""color:Blue;"">ELSE</span>
    <span style=""color:Blue;"">PRINT</span> <span style=""color:#A31515;"">&#39;ERROR: The current user cannot insert data into Person.Address.&#39;</span>;

<span style=""color:Blue;"">IF</span> <span style=""color:Blue;"">PERMISSIONS</span>(<span style=""color:Blue;"">OBJECT_ID</span>(<span style=""color:#A31515;"">&#39;AdventureWorks.Person.Address&#39;</span>,<span style=""color:#A31515;"">&#39;U&#39;</span>))&amp;0x80000=0x80000
    <span style=""color:Blue;"">PRINT</span> <span style=""color:#A31515;"">&#39;INSERT on Person.Address is grantable.&#39;</span>
<span style=""color:Blue;"">ELSE</span>
    <span style=""color:Blue;"">PRINT</span> <span style=""color:#A31515;"">&#39;You may not GRANT INSERT permissions on Person.Address.&#39;</span>;
</pre></div>";

                string actual = new CodeColorizer().Colorize(sourceText, Languages.Sql);

                Assert.Equal(expected, actual);
            }

            [Fact]
            public void WillStyleSuserNameFunction()
            {
                string sourceText =
@"SELECT SUSER_NAME(1)";
                string expected =
@"<div style=""color:Black;background-color:White;""><pre>
<span style=""color:Blue;"">SELECT</span> <span style=""color:Magenta;"">SUSER_NAME</span>(1)
</pre></div>";

                string actual = new CodeColorizer().Colorize(sourceText, Languages.Sql);

                Assert.Equal(expected, actual);
            }
        }
    }
}