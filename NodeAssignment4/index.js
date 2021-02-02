const express = require('express')
const bodyParser = require('body-parser')
const Pool = require('pg').Pool
const pool = new Pool({
  user: 'postgres',
  host: 'localhost',
  database: 'demo',
  password: 'ayushi',
  port: 5432,
})

const app = express()
const port = 3000

app.use(bodyParser.json())
app.use(
  bodyParser.urlencoded({
    extended: true,
  })
)
app.use('/uploads',express.static('./uploads'));
app.get('/', (request, response) => {
  response.json({ info: 'Assignment-3' })
})

//upload and insert image
var multer  = require('multer');
var storage = multer.diskStorage({
    destination: (req, file, cb) => {
      cb(null, './uploads/images');
    },
    filename: (req, file, cb) => {
      //console.log(file);
      var filetype = '';
      if(file.mimetype === 'image/gif') {
        filetype = 'gif';
      }
      if(file.mimetype === 'image/png') {
        filetype = 'png';
      }
      if(file.mimetype === 'image/jpeg') {
        filetype = 'jpg';
      }
      cb(null, 'image-' + Date.now() + '.' + filetype);
    }
});
var upload = multer({ storage: storage });

app.post('/upload/:id', upload.single('profilepicture'), function (req, res, next) {
  const id = parseInt(req.params.id);
  let filename = req.file.filename;
  var d = new Date();
  let datecreated = d.toDateString(); 
  if (!req.file) {
    res.status(500);
    return next(err);
  } 
  pool.query(' INSERT INTO carimage (carid,imagename,createddate) VALUES($1,$2,$3)', [id, filename,datecreated], (error, reslts) => {
    if (error) {
      throw error
    }
    res.status(201).send(`Car image added successfully`)
  })
})
//Get car details
app.get('/carsImage',function (req, res, next){
  pool.query(' select array_to_string(array_agg($1 || carimage.imagename || $2), $3) car_image , car.id as carId , model."name" as modelName , make."name" as makeName from carimage left join car on carimage.carid = car.id left join model on car.modelid = model.id left join make on car.makeid = make.id GROUP by car.id,model."name",make."name"  ',[ '{image: http://localhost:3000/uploads/images/',' }',';'], (error, results) => {
    if (error) {
      throw error;
    }
    res.status(200).json(results.rows)
  })
})

app.listen(port, () => {
  console.log(`App running on port ${port}.`)
})
