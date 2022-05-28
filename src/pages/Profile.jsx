import {useState, useEffect} from 'react'
import { useHistory } from 'react-router-dom'
import { useUser } from '../contexts/CartContext'

import styled from 'styled-components'
import Announcement from '../components/Announcement'
import Navbar from '../components/Navbar'

import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';

import EmailIcon from '@mui/icons-material/Email';
import PersonIcon from '@mui/icons-material/Person';

import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';

import { profile } from '../apiCalls/User'
import { getOrders } from '../apiCalls/Order'

import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';


const Container = styled.div`
  padding: 50px 100px;
  display: flex;
`;
const CartItemCard = styled.div`
    display: flex;
    border-bottom: 1px solid lightgrey;
`
const CartItemImgContainer = styled.div`
    width: 3em;
    height: 3em;
    padding: 1em;
`
const CartItemImg = styled.img`
    width: 100%;
    height: 100%;
`
const CartItemBody = styled.div`
    display: flex;
    flex-direction: column;
    padding: 1em;

`
const CartItemBodyItem = styled.div`
    display: flex;
    flex-direction: column;
`
const CartItemBodyItemTitle = styled.span`
    display: inline-block;
    font-weight: 500;
    font-size: 1em;
`
const CartItemBodyItemDesc = styled.span`
    display: inline-block;
    font-weight: 400;
    font-size: 1em;
`
const CartItemSummary = styled.div`
    /* font-weight: 400; */
    padding: 1em;
    display: flex;
    flex-direction: column;
`
const CartTotal = styled.div`
    padding: 1em;
    font-weight: 500;
    font-size: 1em;
`


function TabPanel(props) {
    const { children, value, index, ...other } = props;
  
    return (
      <div
        role="tabpanel"
        hidden={value !== index}
        id={`simple-tabpanel-${index}`}
        aria-labelledby={`simple-tab-${index}`}
        {...other}
      >
        {value === index && (
          <Box sx={{ p: 3 }}>
            <Typography>{children}</Typography>
          </Box>
        )}
      </div>
    );
  }

function a11yProps(index) {
    return {
      id: `simple-tab-${index}`,
      'aria-controls': `simple-tabpanel-${index}`,
    };
}

function AllList({email, name}) {
    return (
        <List sx={{ width: '100%', maxWidth: 360, bgcolor: 'background.paper' }}>
            <ListItem>
                <ListItemAvatar>
                    <Avatar>
                        <EmailIcon />
                    </Avatar>
                </ListItemAvatar>
                <ListItemText primary="Email" secondary={email} />
            </ListItem>
            <ListItem>
                <ListItemAvatar>
                    <Avatar>
                        <PersonIcon />
                    </Avatar>
                </ListItemAvatar>
                <ListItemText primary="İsim" secondary={name} />
            </ListItem>
        </List>
  )
}

function CartItemList ({cartItem}) {
    return (
    <CartItemCard>
        <CartItemImgContainer>
            <CartItemImg src={cartItem.product.imageUrl}/>
        </CartItemImgContainer>

        <CartItemBody>

            <CartItemBodyItem>
                <CartItemBodyItemTitle>{cartItem.product.title}</CartItemBodyItemTitle>
            </CartItemBodyItem>

            <CartItemBodyItem>
                <CartItemBodyItemDesc>{`${cartItem.amount} adet`}</CartItemBodyItemDesc>
            </CartItemBodyItem>

            <CartItemBodyItem>
                <CartItemBodyItemDesc>{`${cartItem.unitPrice} TL`}</CartItemBodyItemDesc>
            </CartItemBodyItem>
            
        </CartItemBody>
        <CartItemSummary>
            <CartItemBodyItemTitle>Toplam</CartItemBodyItemTitle>
            <CartItemBodyItemDesc>{`${cartItem.product.price * cartItem.amount} TL`}</CartItemBodyItemDesc>
        </CartItemSummary>
    </CartItemCard>
    )
}

export default function Profile() {
    const context = useUser();
    const [value, setValue] = useState(0);
    const [userDetails, setUserDetails] = useState(null);
    const [orders, setOrders] = useState(null);
    const history = useHistory();
    const [expanded, setExpanded] = useState(false);

    const checkUserLoggedIn = () => {
        // let claims = context.claims;
        // setClaims(claims);
        // if (claims === null){
        //     history.push("/");
        // }
    }


  const handleChangeAccordion = (panel) => (event, isExpanded) => {
    setExpanded(isExpanded ? panel : false);
  };
    const setProfileAsync = async () => {
        if (context.claims){
            let result = await profile(context?.claims.Id);
            setUserDetails(result.data);
        }
    }
    const setOrdersAsync = async () => {
        let result = await getOrders();
        setOrders(result.data);
    }

    useEffect(()=> {
        console.log("useeffect[]a")
        // checkUserLoggedIn();
        setProfileAsync();
    },[context.claims]);

    useEffect(() => {
        setOrdersAsync();
    },[userDetails])

    const handleChange = (event, newValue) => {
        setValue(newValue);
      };

  return (
      <div>
          <Navbar />
          <Announcement />
          <Container>
              {context.claims !==  null && userDetails !== null ? (<Box sx={{ width: '100%' }}>
                  <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                      <Tabs value={value} onChange={handleChange} aria-label="basic tabs example">
                          <Tab label="Hakkında" {...a11yProps(0)} />
                          <Tab label="Siparişler" {...a11yProps(1)} />
                      </Tabs>
                  </Box>
                  <TabPanel value={value} index={0}>
                      <AllList email={context.claims.email} name={`${userDetails.firstName} ${userDetails.lastName}`}/>
                  </TabPanel>
                  <TabPanel value={value} index={1}>
                      {orders && 
                        <div>
                      {console.log(orders)}
                            {orders.map((order, i) => <Accordion expanded={expanded === `panel${i}`} onChange={handleChangeAccordion(`panel${i}`)}>
                          <AccordionSummary
                            expandIcon={<ExpandMoreIcon />}
                            aria-controls="panel1bh-content"
                            id="panel1bh-header"
                          >
                            <Typography sx={{ width: '33%', flexShrink: 0 }}>
                              {`Sipariş ${i+1}`}
                            </Typography>
                            <Typography sx={{ color: 'text.secondary' }}>{order.date}</Typography>
                          </AccordionSummary>
                          <AccordionDetails>
                            <Typography>
                              {order.cartItems.map(cartItem =>
                                  <CartItemList cartItem={cartItem}/>
                              )}
                              <CartTotal>
                                  <span>Toplam Tutar: {order.cartItems.reduce((prev, current) => prev + current.product.price * current.amount, 0)} TL</span>
                              </CartTotal>

                            </Typography>
                          </AccordionDetails>
                        </Accordion>)}
                        
                      </div>
                      }
                  </TabPanel>
              </Box>) : (<></>) 
              }
              
          </Container>
      </div>
    
  )
}
