сгенерить файл миграции
Add-Migration {вроде имя надо написать}

сгенерить sql скрпит 
Script-Migration -Idempotent -output 23_05_2021\Scripts\Migrations\V_1.1.sql в первый
Script-Migration -idempotent -From 20210523223341_test_24{обновлять} -output 23_05_2021\Scripts\Migrations\V_1.1.sql {и тут добавлять индекс} // складывать по пути свистнул из фондирования


//4 добавление строк запросом: 
Begin
Declare @AuthorId int
Declare @BookId int
Select @AuthorId = Id from Authors a where a.Name = 'Ушаков Егор'
Select @BookId = Id from Books b where b.Name = '42'


If (@AuthorId is null)
Begin
 Insert into Authors values('Ушаков Егор')
 Select @AuthorId = SCOPE_IDENTITY()
End

If (@BookId is null)
Begin
Insert into Books (Name, Description, Price) VALUES ( '42','Ответ на главный вопрос жизни', '42')
 Select @BookId = SCOPE_IDENTITY()
End

Begin 
Insert into AuthorBook values(@AuthorId, @BookId)
End 
End
//7 запрос на проверку сумм
select ord.ClientId, Sum(ord.Sum) as Sum from Orders ord 
inner join Clients cli on ord.ClientId = cli.Id
group by ClientId
--

with data as ( 
select ord.ClientId, Sum(ord.Sum) as OrdersSum from Orders ord 
inner join Clients cli on ord.ClientId = cli.Id
group by ClientId
)
select * from data d
where d.OrdersSum < 5000


select * from Clients

select * from Orders