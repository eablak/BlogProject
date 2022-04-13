

** Bir Blank Solutıon projes açtık.

* Solutıon altında ilk etapta sınıflarımızı oluşturmak için Class Library projesi açtık.
* Model isimli projeye => Entities
Entities klasörü => Abstract ve Concrete olarak varlıklarımızı ( kullanıacğımız sınıfları oluşturacağız.)

**EntityTypeConfiguration klasörü açaılır. ( abstract + concrete olarak ikiye ayrılır). Veritabanı tarafında kolonları ayağa kaldıracak ve konfigurasynları yaapacak sınıflar olşturulur.

*** BlogProject . Infrastructure => Class Library projesi açılırç
Infrastructure => Context klasörü => Context sınıfı oluşturulur.
Gerekli olan sqlserver ve tools paketleri indirilir.

Infrastructure => Repositories klasörü =>  Abstract - Concrete - Interfaces olmak üzere klasörler açılır.

* asp.net core ( model- view - controller ) olarak projeye 3. bir katman ekledik.
Database ile bağlantı kuracak olan connectStringimizi appSetting.jsona  ekledik ve göç başlattık.
Migration yapılırken : add-migration isim ( default project : cotextin olduğu katman, + mvc projesi seçili olmalı)
                       update-database

*mvc projesinde Areas isimli klasör oluşturulur. 
Areas => add - area - mvc area - isim verilir ve yapı otomatik oluşturulur.
startup içerisindeki configure e endPoint eklenmeli.
NOT => area içindeki controllerı oluşturduğumuzda her controllerın üzerine area attrıbute eklenmeli.

**kişileri oluşturmadan önce identity kütüphanesini projemize adapte etmeliyiz.

* Model projesine => mic.identity.stores 5.0.12 v nuget kurulmalı.
* Infrastrure => yan context sınıfımızda identitydbcotexten gelmeli + mic.identity.efcore  5.0.12 inmeli.

* StartUpta => configureservise identity kütüphanesi kullanıldığı söylenmeli.
+ (configure+confıgureService) authentication eklenmeli
Appuser sınıfı içine de o sınıfı identityUser ile eşleyebilmek için bir string İdentityId prop eklenmeli.

migration + update database işlemi yapılır.
