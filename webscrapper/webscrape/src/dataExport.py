import sqlite3 as sq
import pandas as pd
import re
connection = sq.connect(r"C:\\Users\\gabim\source\\repos\Software-Engineering-Group-6\backend\Database\\test.db")

df = pd.read_csv(r"C:\\Users\\gabim\source\\repos\Software-Engineering-Group-6\webscrapper\webscrape\src\\output_v3\\output.csv", index_col=False)

df = df.drop(["Unnamed: 0"], axis=1)
df = df.drop(["Repeatability"], axis=1)
df = df.drop(["CourseFees"], axis=1)

df["CourseId"] = df["CourseId"].apply(lambda x: re.sub(" ", "", x))
df.to_sql("Courses", connection, if_exists="append", index=False)

connection.close()