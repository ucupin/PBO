# PassByReferenceForm
--INI TUGAS KULIAH--
1. Buat Database SQLite3 dengan nama passbyref.sqlite3

2. Query SQLite3
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "class" (
	"class_id"	INTEGER NOT NULL,
	"class_name"	VARCHAR(255) NOT NULL,
	PRIMARY KEY("class_id")
);
CREATE TABLE IF NOT EXISTS "gender" (
	"gender_id"	INTEGER NOT NULL,
	"gender_type"	VARCHAR(255) NOT NULL,
	PRIMARY KEY("gender_id")
);
CREATE TABLE IF NOT EXISTS "student" (
	"stud_id"	INTEGER NOT NULL,
	"stud_name"	VARCHAR(255) NOT NULL UNIQUE,
	"stud_gender_id" INTEGER REFERENCES "gender"("gender_id") NOT DEFERRABLE,
	"stud_class_id" INTEGER REFERENCES "class"("class_id") NOT DEFERRABLE,
	PRIMARY KEY("stud_id")
);
INSERT INTO "class" ("class_id","class_name") VALUES (1,'Computer Class'),
 (2,'Geography Class'),
 (3,'Biology Class');
INSERT INTO "gender" ("gender_id","gender_type") VALUES (1,'Male'),
 (2,'Fermale');
INSERT INTO "student" ("stud_id","stud_name", "stud_gender_id", "stud_class_id") VALUES (1,'Riski', 1, 3),
 (2,'Arcueid Brunestud', 2, 1),
 (3,'Violet Evergarden', 2, 2);
COMMIT;

3. Build dan taruh database di debug/net-60
