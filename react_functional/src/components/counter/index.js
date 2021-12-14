import React, { useState, useEffect } from 'react'
import { CounterButton, CounterContainer, CounterText } from './counter.style';

export const Counter = ({title, increment=1}) => {
    const [count, setCount] = useState(0);
    // useEffect(()=> console.log('Chama o effect em todas as alteraçoes do componente, incluindo criacao'))
    // useEffect(()=> console.log('Chama o effect só na criaçao do componente'),[])
    // useEffect(()=> console.log('Chama o effect só qd os stados listados mudam, inclindo criacao do componente'),[count])
    return( <CounterContainer>
        <CounterText>{title} {count} times</CounterText>
        <CounterButton primary onClick={() => setCount(count + increment)}>
          Click me
        </CounterButton>
      </CounterContainer>);
}
