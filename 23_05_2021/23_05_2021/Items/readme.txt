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


задание: 
1) Составить модель для следующих сущностей     15х3
Book
 - Name : string
 - Description : string?
 - Price : decimal
 - Authors : Author[]
Author
 - Name : string
 - Books : Book[]
Client
 - Name : string
 - Email : string
 - Orders : Order[]
Order
 - Date : DateTime
 - Client : Client
 - Sum : decimal
 - Books : Book[]
 1а) Часть модели описать через fluent api, часть через атрибуты, часть через naming convention. 30х3
 1б) (*) некоторые отношения прописать через shodow property 5min
2) Сгенерировать миграционный скрипт и накатить на базу  30 min 
3) (*)Добавить constraint на базу: не может быть Book у которой Authors.count < 1. - не сделал
4) Скриптом вставить несколько записей в таблицы Book и Author  30 min 
5) Засидировать через EF seed несколько клиентов  
6) Реализовать метод, который принимает Order и добавляет его или обновляет через ChangeTracker
7) Написать SQL запрос, возвращающий всех Client, которые за прошлый год закупились меньше чем на 5k (или вообще нет заказов)