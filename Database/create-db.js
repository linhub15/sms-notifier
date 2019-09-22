var mongo = require('mongodb').MongoClient;
var url = "mongodb://localhost:27017/notifier"



mongo.connect(url, {
    useNewUrlParser: true,
    useUnifiedTopology: true
  }, (err, client) => {
  if (err) throw err;

  console.log("creating notifier db...");

  const db = client.db('notifier')
  const messages = db.collection('messages');
  messages.insertOne({content: "Freestyle Movement practice tonight!" })


  console.log("... notifer restore complete!")
  client.close();
});


