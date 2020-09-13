let DB_URI = "mongodb://192.168.0.7:27017/mydb";

if(process.env.MONGO_DB_URI){
    DB_URI = process.env.MONGO_DB_URI;
}

module.exports = {
    DB_URI
};